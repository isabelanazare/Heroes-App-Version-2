import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ErrorHandlerService } from './error-handler.service';
import { catchError } from 'rxjs/operators';
import { UserData } from '../models/user-data';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserDataService extends ErrorHandlerService {
  private _userDataUrl = 'https://localhost:44324/api/user';

  constructor(private http: HttpClient) {
    super();
  }

  public createUser(user: UserData): Observable<UserData> {
    return this.http.post(this._userDataUrl, user).pipe(
      catchError(this.handleError<any>())
    );
  }

  public sendUserToken(token: string): Observable<string> {
    return this.http
      .post(`${this._userDataUrl}/userToken?token=${token}`, {})
      .pipe(
        catchError(this.handleError<any>())
      );
  }

  public resetPassword(
    email: string,
    password: string,
    newPassword: string,
    isForgotten: boolean
  ): Observable<any> {
    return this.http
      .put(
        `${this._userDataUrl}/resetPassword?email=${email}&password=${password}&newPassword=${newPassword}&isForgotten=${isForgotten}`,
        {}
      )
      .pipe(
        catchError(this.handleError<any>())
      );
  }

  public updateUserProfile(user: UserData): Observable<UserData> {
    return this.http.put(this._userDataUrl, user).pipe(
      catchError(this.handleError<any>())
    );
  }
}
