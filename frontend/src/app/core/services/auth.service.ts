import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { GLOBAL } from '@core/config/app.config';
import { EndpointType } from '@core/enums/endpoint-type.enum';

interface LoginRequest {
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl: string = '';
  private loggedIn = new BehaviorSubject<boolean>(false);

  isLoggedIn$ = this.loggedIn.asObservable();

  constructor(private http: HttpClient) {
    this.apiUrl = `${GLOBAL.apiBaseUrl}/${EndpointType.Users}`;
    const token = localStorage.getItem('token');
    if (token) {
      this.loggedIn.next(true);
    }
  }

  login(email: string, password: string): Observable<any> {
    const url = `${this.apiUrl}/login`;
    const body = {
      Usr: email,
      Psw: password,
    };

    return this.http.post<any>(url, body).pipe(
      tap((response) => {
        localStorage.setItem('token', response.token);
        this.loggedIn.next(true);
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
