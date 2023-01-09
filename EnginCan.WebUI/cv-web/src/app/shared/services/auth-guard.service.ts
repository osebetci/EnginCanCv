import { HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { ActivatedRouteSnapshot, CanActivate, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Subject } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { AuthService } from "./auth.service";
import { HttpService } from "./http.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(
    public authService: AuthService,
    public router: Router,
    private titleService: Title,
    private http: HttpService,
    private toastr: ToastrService
  ) { }

  private title = new Subject<string>();
  titlechangeEmitted$ = this.title.asObservable();

  private breadCrumb = new Subject<string>();
  breadCrumbchangeEmitted$ = this.breadCrumb.asObservable();

  private pageId = new Subject<number>()
  pageIdEmitted$ = this.pageId.asObservable();


  async canActivate(route: ActivatedRouteSnapshot): Promise<boolean> {
    return this.authService.isAuthenticated().then(isOk => {
      if (!isOk) {
        this.router.navigate([environment.loginPath], {
          queryParams: { from: location.pathname + location.search }
        });
        return false;
      } else {

        const headers = new HttpHeaders({'Referrer':`${route['_routerState']['url']}`});
        // if (route.data.pageId) {
          this.http
            .get(environment.api, {
              url: `PagePermission/CanActivate/${route.data.pageId? route.data.pageId: 0}`,
              version: '1.0',
              headers: headers
            })
            .pipe(map(res => res))
            .subscribe(res => {
              if (res.isAuthority) {
                this.pageId.next(res.id)
                this.titleService.setTitle(res.name);
                this.title.next(res.name);
                this.breadCrumb.next(res.breadCrumb);
                return true;
              } else {
                this.toastr.error(res.name, 'Hata');
                this.router.navigate(['/not-found']);
                return false;
              }
            });
        // }

        return true;
      }
    });
  }
}

@Injectable({
    providedIn: 'root'
  })
  export class IsLoggedIn {
    constructor(private router: Router, private authService: AuthService) { }
  
    resolve(): void {
      this.authService
        .getLoginUser()
        .subscribe(res => this.router.navigate([res.firstFireLink]));
    }
  }