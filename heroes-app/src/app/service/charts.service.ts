import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChartHero } from '../models/chart-hero';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class ChartsService extends ErrorHandlerService{
  private _chartsUrl = 'https://localhost:44324/api/Charts';

  constructor(private http: HttpClient) {
    super();
  }

  public getHeroesPowerBarChart(): Observable<ChartHero> {
    return this.http.get<any>(`${this._chartsUrl}/heroes`)
      .pipe(
        catchError(this.handleError<any>('getHeroesPowerChartBar', []))
      )
  }

  public getPowerBarChart(): Observable<ChartHero> {
    return this.http.get<any>(`${this._chartsUrl}/powers`)
      .pipe(
        catchError(this.handleError<any>('getPowerBarChart', []))
      )
  }
}
