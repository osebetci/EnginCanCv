import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class JwtService {
  getToken(): string {
    return window.localStorage[environment.api.token];
  }

  saveToken(token: string) {
    window.localStorage[environment.api.token] = token;
  }

  destroyToken() {
    window.localStorage.removeItem(environment.api.token);
  }
}
