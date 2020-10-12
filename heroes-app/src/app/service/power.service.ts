import { Injectable } from '@angular/core';
import { ErrorHandlerService } from './error-handler.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PowerRowData } from '../models/power-row-data';
import {  catchError } from 'rxjs/operators';
import { Power } from '../models/power';
import { Constants } from '../utils/constants';

@Injectable({
  providedIn: 'root',
})
export class PowerService extends ErrorHandlerService {
  private _powersUrl = 'https://localhost:44324/api/Powers';

  constructor(private http: HttpClient) {
    super();
  }

  public getPowersDataTable(): Observable<PowerRowData[]> {
    return this.http.get<PowerRowData[]>(`${this._powersUrl}/tableData`).pipe(
      catchError(this.handleError<any>('getPoers', []))
    );
  }

  public getPower(id: number): Observable<Power> {
    return this.http
      .get<Power>(`${this._powersUrl}/${id}`)
      .pipe(catchError(this.handleError<any>('getPoers', [])));
  }

  public deletePower(id: number): Observable<any> {
    return this.http.delete<any>(`${this._powersUrl}/${id}`).pipe(
      catchError(this.handleError<any>('deletePower', []))
    );
  }

  public getPowers(): Observable<Power[]> {
    return this.http.get<Power[]>(this._powersUrl).pipe(
      catchError(this.handleError<any>())
    );
  }

  public savePower(power: Power): Observable<Power> {
    return this.http.post(this._powersUrl, power).pipe(
      catchError(this.handleError<any>())
    );
  }

  public updatePower(power: Power): Observable<Power> {
    return this.http.put(this._powersUrl, power).pipe(
      catchError(this.handleError<any>())
    );
  }
}
