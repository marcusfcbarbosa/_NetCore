import { Injectable } from '@angular/core';
import { Constants } from './../_util/Constants';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { User } from '../_models/User';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseURL = Constants.URL_BASE_AUTHENTICATION;
  _context = Constants.CONTEXT;
  jwtHelper = new JwtHelperService();
  decodeToken: any;

constructor(private http: HttpClient) { 


}

  login(model: any){
      return this.http.post(`${this.baseURL}login`, model).pipe(
        map((response: any) => {
            const user = response;
            if(user){
              localStorage.setItem('token', user.token);
              this.decodeToken = this.jwtHelper.decodeToken(user.token);
            }
        })
      );
  }

  register(model: User){
    model.context = this._context;
    model.Name = model.email;
    return this.http.post(`${this.baseURL}register`, model);

  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return this.jwtHelper.isTokenExpired(token);
  }

}
