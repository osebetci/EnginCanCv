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

  getAllAbout() {
    return this.http
      .get(environment.api, {
        url: 'About/GetAllAbout',
        version: '1.0',
      })
      .pipe(
        map((data) => {
          return data;
        })
      );
  }

  postAbout(model) {
    return this.http
      .post(environment.api, {
        url: 'About/PostAbout/',
        version: '1.0',
      },model)
      .pipe(
        map((data) => {
          return data;
        })
      );
  }
  updateAbout(model) {
    return this.http
      .post(environment.api, {
        url: 'About/UpdateAbout/',
        version: '1.0',
      },model)
      .pipe(
        map((data) => {
          return data;
        })
      );
  }
  deleteAbout(id) {
    return this.http
      .get(environment.api, {
        url: 'About/DeleteAbout/'+id,
        version: '1.0',
      })
      .pipe(
        map((data) => {
          return data;
        })
      );
  }

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
