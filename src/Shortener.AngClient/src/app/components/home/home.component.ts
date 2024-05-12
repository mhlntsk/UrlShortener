import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { UrlComponent } from '../url/url.component';
import { Url } from '../../shared/Url';
import { GetDataService } from '../../services/urls.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    UrlComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  urlsList: Url[] = [];
  getDataService: GetDataService = inject(GetDataService);

  constructor() {
    this.getDataService.getAllShorteredUrls().then((urlsList: Url[]) => {
      this.urlsList = urlsList;
    });
  }
}
