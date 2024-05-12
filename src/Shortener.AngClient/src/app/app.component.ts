import { Component } from '@angular/core';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { RouterModule } from '@angular/router';

import { UrlComponent } from './components/url/url.component';
import { UrlDetailsComponent } from './components/url-details/url-details.component';
import { ReactiveFormsModule } from '@angular/forms';
import { GetDataService } from './services/urls.service';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports:[
    RegistrationComponent,
    LoginComponent,
    HomeComponent,
    RouterModule, 
    
    UrlComponent,
    UrlDetailsComponent,
    ReactiveFormsModule
  ],
  providers: [
    GetDataService
  ],
})
export class AppComponent {
  title = 'Shortener';
}
