import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UpdateUser } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  verify(id: number){
    return this.http.patch(this.baseUrl + '/v1/user/verify/' + id , this.getHttpHeader());
  }

  deny(id: number){
    return this.http.patch(this.baseUrl + '/v1/user/deny/' + id, this.getHttpHeader());
  }

  getSeller(){
    return this.http.get(this.baseUrl + '/v1/user/sellers', this.getHttpHeader());
  }

  getUserDetails(id: number){
    return this.http.get(this.baseUrl + '/v1/user/' + id, this.getHttpHeader());
  }

  update(user: UpdateUser){
    return this.http.patch(this.baseUrl + '/v1/user/update', user, this.getHttpHeader());
  }


  getHttpHeader(): { headers: HttpHeaders; }{
    const httpOptions = {
      headers: new HttpHeaders({
        Accept: "application/json"
      })
    };
    return httpOptions;
  }
}
