import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { Url } from '../../shared/Url';
import { UrlService } from '../../services/url.service'
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  urlsList: any;
  urlService: UrlService = inject(UrlService);
  urlInput: string = '';

  constructor(public authService: AuthService) { }

  isAllowed(id: number): boolean {
    if (!localStorage.getItem('userId') && !localStorage.getItem('userRole')) {
      return false
    }

    if (localStorage.getItem('userRole') == "admin") {
      return true;
    }

    if (localStorage.getItem('userId') == String(id)) {
      return true
    }

    return false
  }

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
        userId: Number(localStorage.getItem('userId')),
      }

      this.urlService.addUrl(urlToPush).subscribe(() => {
        this.getUrls();
      },
        (error) => {
          if (error.status === 400) {
            alert(`Bad request: ${error.error.detail}`);
          } else if (error.status === 409) {
            alert(`This link already exists - ${error.error.detail}`)
          } else if (error.status === 500) {
            alert(`Internal server error: ${error.error.detail}`)
          } else {
            alert(`An error occurred during adding the resource: ${error.error.detail}`);
          }
        });
    }

    this.urlInput = '';
  }

  deleteUrl(id: number) {
    this.urlService.deleteUrl(id).subscribe(() => {
      this.getUrls();
    },
      (error) => {
        if (error.status === 404) {
          alert(`Bad request: ${error}`);
        } else if (error.status === 409) {
          alert(`This link already exists - ${error}`)
        } else {
          alert(`An error occurred while deleting: ${error}`);
          console.error('An error occurred while deleting:', error);
        }
      }
    );
  }


}
