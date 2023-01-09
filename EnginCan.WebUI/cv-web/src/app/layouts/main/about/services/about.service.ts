import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { HttpService } from 'src/app/shared/services/http.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AboutService {
  constructor(private http: HttpService, private router: Router) {}

  getAbout(id:number){
    return this.http
    .get(environment.api, {
      url: 'About/GetById/'+id,
      version: '1.0',
    })
    .pipe(
      map((data) => {
        return data;
      })
    );
  }
}
