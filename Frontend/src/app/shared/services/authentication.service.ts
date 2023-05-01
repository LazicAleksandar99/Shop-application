import { Injectable } from '@angular/core';
import { LoginUser, RegistrationUser } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  login(user: LoginUser){
    return this.http.post(this.baseUrl + 'users/login', user);
  }

  register(user: RegistrationUser){
    return this.http.post(this.baseUrl + 'users/registration', user);
  }
}
