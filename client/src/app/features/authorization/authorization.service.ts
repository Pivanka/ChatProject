import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, tap } from 'rxjs';
import { API_URL } from 'src/app/app-injection-tokens';
import { SIGN_IN_REQUEST_BODY, SIGN_UP_REQUEST_BODY, RESPONSE } from 'src/app/models/authorization.models';

import { environment, ACCESS_TOKEN_KEY } from 'src/environments/environments';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  baseUrl: string = environment.api;

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  login(body: SIGN_IN_REQUEST_BODY): Observable<RESPONSE>{
    return this.http.post<RESPONSE>(this.baseUrl + 'api/auth/login', body).pipe(
      tap(token => {
        if(token.isSuccess)
        {
          localStorage.setItem(ACCESS_TOKEN_KEY, token.message);
          if(token.user) {
            localStorage.setItem('userName', token.user.userName);
            localStorage.setItem('email', token.user.email);
          }
        }
      })
    )
  }

  isAuthenticated(): boolean{
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token ? true : false;
  }

  logout():  Observable<any>{
    const token = this.getToken();
    const url: string = this.baseUrl + 'api/auth/logout';
    var headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':  `Bearer ${token}`
    })
    const requestOptions = { headers: headers };
    localStorage.clear();
    this.router.navigate(['signin']);
    return this.http.get(url, requestOptions);
  }

  signup(body: SIGN_UP_REQUEST_BODY): Observable<RESPONSE>{
    const url: string = this.baseUrl + 'api/auth/register';
    return this.http.post<RESPONSE>(url, body).pipe(
      tap(token => {
        if(token.isSuccess)
        {
          alert(token.message);
        }
      })
    )
  }

  getToken(){
    return localStorage.getItem(ACCESS_TOKEN_KEY) as string;
  }
}
