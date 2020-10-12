import { Injectable } from '@angular/core';
import { ErrorHandlerService } from './error-handler.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ElementType } from '../models/element-type';
import {  catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ElementService extends ErrorHandlerService {
  private _elementsUrl = 'https://localhost:44324/api/Elements';

  constructor(
    private http: HttpClient
  ) {
    super();
  }

  public getElements(): Observable<ElementType[]> {
    return this.http.get<ElementType[]>(this._elementsUrl)
      .pipe(
        catchError(this.handleError<any>())
      );
  }
}
