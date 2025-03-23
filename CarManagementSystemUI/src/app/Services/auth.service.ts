import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7111/api/Auth/Login';

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post(this.apiUrl, { username, password }).pipe(
      tap((response: any) => {
        if (response && response.token) {
          localStorage.setItem('auth_token', response.token);
        }
      })
    );
  }

  getToken(): string {
    return localStorage.getItem('auth_token') || '';
  }

  setToken(token: string): void {
    localStorage.setItem('auth_token', token);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem('auth_token');
  }
}
