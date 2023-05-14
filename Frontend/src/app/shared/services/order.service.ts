import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

baseUrl = environment.baseUrl;
constructor(private http: HttpClient) { }

getAllOrders(){
  return this.http.get(this.baseUrl + '/v1/order', this.getHttpHeader());
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
