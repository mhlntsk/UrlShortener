import { Injectable } from '@angular/core';
import { Url } from '../shared/Url';

@Injectable({
  providedIn: 'root'
})
export class GetDataService {

  private getUrlsLink: string = "https://localhost:7286/api/UrlApi/urls";

  async getAllShorteredUrls() : Promise<Url[]> {
    const data = await fetch(this.getUrlsLink);
    return await data.json() ?? [];
  }

  async getShorteredUrlsById(id: number) : Promise<Url | undefined> {
    const data = await fetch(`${this.getUrlsLink}/${id}`);
    return await data.json() ?? {};
  }

  
}
