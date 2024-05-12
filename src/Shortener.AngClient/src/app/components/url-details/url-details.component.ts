import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GetDataService } from '../../services/urls.service';
import { Url } from '../../shared/Url';

@Component({
  selector: 'app-url-detail',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './url-details.component.html',
  styleUrl: './url-details.component.css'
})
export class UrlDetailsComponent {
  getDataService = inject(GetDataService);
  url: Url | undefined;

  route: ActivatedRoute = inject(ActivatedRoute);

  constructor() {
    const urlId = Number(this.route.snapshot.params['id']);

    this.getDataService.getShorteredUrlsById(urlId).then(url => {
      this.url = url;
    });
  }
}
