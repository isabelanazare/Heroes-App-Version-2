import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserData } from '../models/user-data';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { Constants } from '../utils/constants';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private _currentUserSubject: BehaviorSubject<UserData>;
  public currentUser: Observable<UserData>;
  private _authUrl = 'https://localhost:44324/api/Authentication';

  constructor(
    private http: HttpClient,
    private errorHandler: ErrorHandlerService
  ) {
    this._currentUserSubject = new BehaviorSubject<UserData>(
      JSON.parse(localStorage.getItem(Constants.AUTH_CURRENT_USER_KEY))
    );
    this.currentUser = this._currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserData {
    return this._currentUserSubject.value;
  }

  public setCurrentUserValue(user: UserData) {
    localStorage.setItem(Constants.AUTH_CURRENT_USER_KEY, JSON.stringify(user));
    localStorage.setItem('token', user.token);
    this._currentUserSubject.next(user);
  }

  public login(username: string, password: string) {
    return this.http
      .post<any>(`${this._authUrl}/authenticate`, { username, password })
      .pipe(
        map((user) => {
          localStorage.setItem(
            Constants.AUTH_CURRENT_USER_KEY,
            JSON.stringify(user)
          );
          localStorage.setItem('token', user.token);
          this._currentUserSubject.next(user);
          return user;
        }),
        catchError(this.errorHandler.handleError<any>())
      );
  }

  public logout() {
    localStorage.removeItem(Constants.AUTH_CURRENT_USER_KEY);
    localStorage.removeItem('token');
    localStorage.removeItem('battleCount');
    this._currentUserSubject.next(null);
  }

  public isAuthenticated() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/json; charset=utf-8'
    );

    return this.http.post<any>(
      `${this._authUrl}/isAuthenticated`,
      { token },
      { headers: headers }
    );
  }
}
