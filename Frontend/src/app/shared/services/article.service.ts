import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Article, NewArticle } from '../models/article';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  
  baseUrl = environment.baseUrl;
  token!: any;
  sellerId!: any;
  constructor(private http: HttpClient,
              private authService: AuthService) { }

  create(user: NewArticle){
    this.token = localStorage.getItem('token');
    user.userId = this.authService.getUserId(this.token);
    return this.http.post(this.baseUrl + '/v1/article/create', user, this.getHttpHeader());
  }

  update(article: Article){
    return this.http.patch(this.baseUrl + '/v1/article/update', article, this.getHttpHeader());
  }

  getArticalDetails(id: number){
    return this.http.get(this.baseUrl + '/v1/article/details/' + id, this.getHttpHeader());
  }

  getAllArticles(){
    return this.http.get(this.baseUrl + '/v1/article', this.getHttpHeader());
  }

  getSellerArticles(id: number){
    return this.http.get(this.baseUrl + '/v1/article/' + id, this.getHttpHeader());
  }

  deleteArticle(id: number){
    this.token = localStorage.getItem('token');
    this.sellerId = this.authService.getUserId(this.token);
    return this.http.delete(this.baseUrl + '/v1/article/delete/' + id + '/' + this.sellerId , this.getHttpHeader());
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
