import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders, } from '@angular/common/http';
import { Url } from '../shared/Url';

@Injectable({
  providedIn: 'root'
})
export class UrlService {

  constructor(private httpClient: HttpClient){}
  //httpClient: HttpClient = inject(HttpClient);
  
  private serverLink: string = "https://localhost:7286/";

  getUrls() {
    return this.httpClient.get(`${this.serverLink}api/Url`)
  }
  
  getUrl(id: number) {
    return this.httpClient.get(`${this.serverLink}api/Url/${id}`)
  }

  addUrl(url: Url) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('accessToken')}`
    });
    return this.httpClient.post(`${this.serverLink}api/Url`, url, { headers: headers })
  }

  deleteUrl(id: number) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('accessToken')}`
    });
    return this.httpClient.delete(`${this.serverLink}api/Url/${id}`, { headers: headers })
  }
}
