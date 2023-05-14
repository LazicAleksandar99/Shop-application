import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NewArticle } from '../models/article';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  create(user: NewArticle){
    return this.http.post(this.baseUrl + '/v1/article/create', user, this.getHttpHeader());
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
