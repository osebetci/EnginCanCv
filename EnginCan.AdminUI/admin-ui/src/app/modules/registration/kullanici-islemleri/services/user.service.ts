import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { HttpService } from 'src/app/shared/services/http.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpService, private router: Router) {}

  getAllUser() {
    return this.http
      .get(environment.api, {
        url: 'User/GetAllUser',
        version: '1.0',
      })
      .pipe(
        map((data) => {
          return data;
        })
      );
  }
  getAllUsersForSelection() {
    return this.http
      .get(environment.api, {
        url: 'User/GetAllUsersForSelection',
        version: '1.0',
      })
      .pipe(
        map((data) => {
          return data;
        })
      );
  }
  postUser(model) {
    return this.http
      .post(environment.api, {
        url: 'User/PostUser/',
        version: '1.0',
      },model)
      .pipe(
        map((data) => {
          return data;
        })
      );
  }
  updateUser(model) {
    return this.http
      .post(environment.api, {
        url: 'User/UpdateUser/',
        version: '1.0',
      },model)
      .pipe(
        map((data) => {
          return data;
        })
      );
  }
  deleteUser(id) {
    return this.http
      .get(environment.api, {
        url: 'User/DeleteUser/'+id,
        version: '1.0',
      })
      .pipe(
        map((data) => {
          return data;
        })
      );
  }

  getUser(id:number){
    return this.http
    .get(environment.api, {
      url: 'User/GetById/'+id,
      version: '1.0',
    })
    .pipe(
      map((data) => {
        return data;
      })
    );
  }
}
