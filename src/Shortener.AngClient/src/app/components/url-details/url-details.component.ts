import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlService } from '../../services/url.service';
import { Url } from '../../shared/Url';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-url-detail',
  standalone: true,
  imports: [
    CommonModule,
  ],
  templateUrl: './url-details.component.html',
  styleUrl: './url-details.component.css'
})
export class UrlDetailsComponent {
  urlService = inject(UrlService);
  url: any;

  route: ActivatedRoute = inject(ActivatedRoute);

  constructor() {
    const urlId = Number(this.route.snapshot.params['id']);

    this.urlService.getUrl(urlId).subscribe(url => {
      this.url = url;
    });
  }
}
