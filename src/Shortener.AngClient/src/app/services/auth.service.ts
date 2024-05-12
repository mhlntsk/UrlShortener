import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7286/api';

  constructor(private http: HttpClient) {}

  register(userData: { 
      firstName: string; 
      lastName: string; 
      email: string; 
      password: string; 
      passwordConfirmation: string 
    }): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/auth/register`, userData);
  }
  
  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/auth/login`, credentials).pipe(
      map(response => {
        localStorage.setItem('accessToken', response.accessToken);
        return response;
      })
    );
  }

  logout(): void {
    localStorage.removeItem('accessToken');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('accessToken');
  }
}
