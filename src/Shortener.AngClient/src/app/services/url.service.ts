import { Injectable, inject } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { Url } from '../shared/Url';

@Injectable({
  providedIn: 'root'
})
export class UrlService {

  constructor(private httpClient: HttpClient){}
  //httpClient: HttpClient = inject(HttpClient);
  
  private serverLink: string = "https://localhost:7286/";

  async getAllUrls(): Promise<Url[]> {
    const data = await fetch(`${this.serverLink}/api/Url`);
    return await data.json() ?? [];
  }

  async getUrlById(id: number): Promise<Url | undefined> {
    const data = await fetch(`${this.serverLink}/api/Url/${id}`);
    return await data.json() ?? {};
  }

  getUrls() {
    return this.httpClient.get(`${this.serverLink}api/Url`)
  }
  
  getUrl(id: number) {
    return this.httpClient.get(`${this.serverLink}api/Url/${id}`)
  }

  addUrl(url: Url) {
    return this.httpClient.post(`${this.serverLink}api/Url`, url)
  }

  deleteUrl(id: number) {
    return this.httpClient.delete(`${this.serverLink}api/Url/${id}`)
  }
}
