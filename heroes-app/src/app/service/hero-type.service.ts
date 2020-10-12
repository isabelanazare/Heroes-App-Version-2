import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ErrorHandlerService } from './error-handler.service';
import { Observable } from 'rxjs';
import { HeroType } from '../models/hero-type';
import {  catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class HeroTypeService extends ErrorHandlerService {
  private _heroTypeUrl = 'https://localhost:44324/api/HeroType';

  constructor(private http: HttpClient) {
    super();
  }

  public getTypes(): Observable<HeroType[]> {
    return this.http.get<any>(this._heroTypeUrl).pipe(
      catchError(this.handleError<any>())
    );
  }
}
