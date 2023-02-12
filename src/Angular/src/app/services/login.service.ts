import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import baseUrl, {Constants} from './helper';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  public loginStatusSubject = new Subject<boolean>();

  constructor(private http: HttpClient) {}
  public getCurrentUser(token) {

    let h = new HttpHeaders().set('Authorization', ' Bearer ' + token);

    return this.http.get(`${baseUrl}/User/current-user`,{ headers: h });
  }
  
  // generate token

  public generateToken(loginData: any) {
    return this.http.post(`${baseUrl}/User/login`, loginData);
  }

  // login user: set token in localStorage
  public loginUser(token) {

    localStorage.setItem('token', token);

    return true;
  }

  // isLogin: user is logged in or not
  public isLoggedIn() {
    const tokenStr = localStorage.getItem('token');
    if (tokenStr == undefined || tokenStr == '' || tokenStr == null) {
      return false;
    } else {
      return true;
    }
  }

  // logout : remove token from local storage
  public logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    return true;
  }

  // get token
  public getToken() {
    return localStorage.getItem('token');
  }

  // set userDetail
  public setUser(user) {
    localStorage.setItem('user', JSON.stringify(user));
  }

  // getUser
  public getUser() {
    const userStr = localStorage.getItem('user');
    if (userStr != null) {
      return JSON.parse(userStr);
    } else {
      this.logout();
      return null;
    }
  }

  // get user role test please goto safari please

  public getUserRole() {
    const user = this.getUser();
    return user.roles[0];// we hanve not this fiels have a roles let me test
  }
}
