import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, catchError, distinctUntilChanged } from 'rxjs/operators';
import { HttpService } from './http.service';
import { environment } from 'src/environments/environment';
import { JwtService } from './jwt.service';
import { BehaviorSubject, Observable } from 'rxjs';
import {
  ResponseLogin,
  Login,
  LoginUser,
} from 'src/app/shared/core/dto/general.dto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<ResponseLogin>(
    {} as ResponseLogin
  );
  public currentUser = this.currentUserSubject.asObservable().pipe();
  public currentUserObservable = this.currentUser;

  constructor(
    private http: HttpService,
    private router: Router,
    private jwtService: JwtService
  ) {}

  // Verify JWT in localstorage with server & load user's info.
  // This runs once on application startup.
  syncUser() {
    // If JWT detected, attempt to get & store user's info
    if (this.jwtService.getToken()) {
      this.getLoginUser().subscribe();
    } else {
      this.logout();
    }
  }

  getToken = (): string => {
    return this.jwtService.getToken();
  };

  // token data check is null or undefined
  public async isAuthenticated() {
    return !!this.jwtService.getToken();
  }
  login(login: Login) {
    return this.http
      .post(
        environment.api,
        { url: 'User/Authenticate', version: '1.0' },
        login
      )
      .pipe(
        map(
          (res) => {
            this.setAuth(res.data);
            return res;
          },
          (err: any) => err
        )
      );
  }

  getLoginUser() {
    return this.http
      .get(environment.api, {
        url: 'User/LoginUser',
        version: '1.0',
      })
      .pipe(
        map((data) => {
          this.currentUserSubject.next(<ResponseLogin>{
            token: this.jwtService.getToken(),
            loginUser: data,
          });
          return data;
        }),
        catchError((res) => {
          this.logout();
          return res;
        })
      );
  }

  getAllPage(){
    return this.http
    .get(environment.api, {
      url: 'Page/GetPermissionPage',
      version: '1.0',
    })
    .pipe(
      map((data) => {      
        return data.data;
      })
    );
  }

  setAuth(responseLogin: ResponseLogin) {
    // Save JWT sent from server in localstorage
    this.jwtService.saveToken(responseLogin.token);
    // Set current user data into observable
    this.currentUserSubject.next(responseLogin);
  }

  logout(): void {
    // Remove JWT from localstorage
    this.jwtService.destroyToken();
    this.currentUserSubject.next({} as ResponseLogin);
    this.router.navigate([environment.loginPath]);
  }

  // Sayfayı yenileyerek logout yapar
  logoutWithRefresh(): void {
    // Remove JWT from localstorage
    this.jwtService.destroyToken();
    // Set current user to an empty object
    this.currentUserSubject.next({} as ResponseLogin);
    this.router
      .navigate([environment.loginPath])
      .then(() => window.location.reload());
  }
}
