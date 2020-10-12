import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Villain } from '../models/villain';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { VillainRowData } from '../models/villain-row-data';
import { ErrorHandlerService } from './error-handler.service';
import { Constants } from '../utils/constants';

@Injectable({
  providedIn: 'root',
})
export class VillainService extends ErrorHandlerService {
  private _villainsUrl = 'https://localhost:44324/api/villains';

  constructor(private http: HttpClient) {
    super();
  }

  public getVillainById(id: number): Observable<Villain> {
    return this.http
      .get<Villain>(`${this._villainsUrl}/${id}`)
      .pipe(catchError(this.handleError<any>()));
  }

  public getVillains(): Observable<Villain[]> {
    return this.http
      .get<Villain[]>(this._villainsUrl)
      .pipe(catchError(this.handleError<Villain[]>('getVillains', [])));
  }

  public getVillainsRowData(): Observable<VillainRowData[]> {
    return this.http
      .get<VillainRowData[]>(`${this._villainsUrl}/villainsRowData`)
      .pipe(catchError(this.handleError<any>('getVillainsRowData', [])));
  }

  public changeVillainImage(id: number, avatarPath: string): Observable<any> {
    let url = `${this._villainsUrl}/avatar?id=${id}&avatarPath=${avatarPath}`;

    return this.http.put(url, {}).pipe(catchError(this.handleError<any>()));
  }

  public addVillain(villain: Villain): Observable<Villain> {
    return this.http
      .post<Villain>(this._villainsUrl, villain)
      .pipe(catchError(this.handleError<any>()));
  }

  public updateVillain(villain: Villain): Observable<Villain> {
    return this.http
      .put<Villain>(`${this._villainsUrl}`, villain)
      .pipe(catchError(this.handleError<any>()));
  }

  public deleteVillain(id: number): Observable<Villain> {
    const url = `${this._villainsUrl}/${id}`;
    return this.http
      .delete<Villain>(url)
      .pipe(catchError(this.handleError<Villain>('deleteVillain')));
  }

  public changeVillainLocation(
    id: number,
    latitude: number,
    longitude: number
  ): Observable<any> {
    return this.http
      .put<any>(
        `${this._villainsUrl}/location?id=${id}&latitude=${latitude}&longitude=${longitude}`,
        {}
      )
      .pipe(catchError(this.handleError<any>()));
  }
}
