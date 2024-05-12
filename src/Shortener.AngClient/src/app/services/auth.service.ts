import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }
  //httpClient: HttpClient = inject(HttpClient);

  private serverLink: string = "https://localhost:7286/";

  register(userData: { firstName: string; lastName: string; email: string; password: string; passwordConfirmation: string }) {
    return this.httpClient.post(`${this.serverLink}api/auth/register`, userData);
  }

  login(credentials: { email: string; password: string, rememberMe: boolean, returnUrl: string }) : Observable<any> {
    return this.httpClient.post<any>(`${this.serverLink}api/auth/login`, credentials);
  }

  logout(): void {
    localStorage.removeItem('accessToken');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('accessToken');
  }
}
