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

getOrderHistory(id: number){
  return this.http.get(this.baseUrl + '/v1/order/history/' + id, this.getHttpHeader());
}

getHttpHeader(): { headers: HttpHeaders; }{
  const httpOptions = {
    headers: new HttpHeaders({
      Accept: "application/json",
      Authorization: 'Bearer '+ localStorage.getItem('token')
    })
  };
  return httpOptions;
}

}
