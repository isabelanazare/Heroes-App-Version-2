import { Injectable } from '@angular/core';
import { ErrorHandlerService } from './error-handler.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {  catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FileService extends ErrorHandlerService {
  private _userDataUrl = 'https://localhost:44324/api/File';

  constructor(
    private http: HttpClient
  ) {
    super();
  }

  public getUserAvatarPath(formData: FormData, id: number): Observable<FormData> {
    return this.http.post(`${this._userDataUrl}/userAvatar?id=${id}`, formData)
      .pipe(
        catchError(this.handleError<any>())
      );
  }

  public getHeroAvatarPath(formData: FormData, id: number): Observable<any> {
    return this.http.post(`${this._userDataUrl}/heroAvatar?id=${id}`, formData)
      .pipe(
        catchError(this.handleError<any>())
      );
  }
}
