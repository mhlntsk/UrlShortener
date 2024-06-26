import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders, } from '@angular/common/http';
import { Url } from '../shared/Url';
import { tap } from 'rxjs/internal/operators/tap';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  httpClient: HttpClient = inject(HttpClient);
  
  private serverLink: string = "https://localhost:7286/";

  getUrls() {
    return this.httpClient.get(`${this.serverLink}api/Url`)
  }
  
  getUrl(id: number) {
    return this.httpClient.get(`${this.serverLink}api/Url/${id}`)
    .pipe(tap(_ => console.log("Url received: ", _)));
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
