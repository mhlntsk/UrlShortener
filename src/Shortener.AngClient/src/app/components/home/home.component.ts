import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { UrlComponent } from '../url/url.component';
import { Url } from '../../shared/Url';
import { UrlService } from '../../services/url.service'
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    UrlComponent,
    HttpClientModule,
    FormsModule,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  urlsList: any;
  urlService: UrlService = inject(UrlService);
  urlInput: string = '';

  ngOnInit(): void {
    this.getUrls();
  }

  getUrls() {
    this.urlService.getUrls().subscribe(urlsList => {
      this.urlsList = urlsList;
    });
  }

  addNewUrl() {
    
    if (this.urlInput) {
      const urlToPush: Url = {
        id: undefined,
        fullUrl: this.urlInput,
        shortUrl: "",
        createdDate: new Date(),
        lastAppeal: undefined,
        numberOfAppeals: 0,
        userId: 1,
      }

      this.urlService.addUrl(urlToPush).subscribe(() => {
        this.getUrls();
      },
      (error) => {
        if (error.status === 400) {
          alert("Resource not found");
        } else if (error.status === 409 ) {
          alert(`This link already exists - ${error.error.detail}.`)
        } else if (error.status === 500 ) {
          alert(`Internal server error.`)
        } else {
          alert(`An error occurred while deleting the resource: ${error}`);
        }
      });
    }

    this.urlInput = '';
  }

  deleteUrl0(id: number) {
    this.urlService.deleteUrl(id).subscribe(del => {
      this.getUrls();
    })
  }

  deleteUrl(id: number) {
    this.urlService.deleteUrl(id).subscribe(() => {
        this.getUrls();
      },
      (error) => {
        if (error.status === 404) {
          alert("Resource not found");
          console.log("Resource not found");
        } else if (error.status === 409 ) {
          alert(`This link already exists - ${error.detail}`)
          console.log(`This link already exists - ${error.detail}`);
        } else {
          console.error('Помилка при видаленні ресурсу:', error);
        }
      }
    );
  }
  

}
