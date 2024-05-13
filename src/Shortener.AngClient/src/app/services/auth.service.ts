import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  httpClient: HttpClient = inject(HttpClient);

  private serverLink: string = "https://localhost:7286/";

  register(userData: { firstName: string; lastName: string; email: string; password: string; passwordConfirmation: string }) {
    return this.httpClient.post(`${this.serverLink}api/auth/register`, userData);
  }

  login(credentials: { email: string; password: string, rememberMe: boolean, returnUrl: string }): Observable<any> {
    return this.httpClient.post<any>(`${this.serverLink}api/auth/login`, credentials);
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('userRole');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('accessToken');
  }
}
