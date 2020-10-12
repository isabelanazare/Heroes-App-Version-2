import { VillainPowers } from './../models/villain-powers';
import { MainPowerChange } from './../models/main-power-change';
import { HeroPower } from './../models/hero-power';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class HeroPowerService extends ErrorHandlerService {
  private _url = 'https://localhost:44324/api/heropowers';

  constructor(private http: HttpClient) {
    super();
  }

  public getHeroPowersForHero(heroId: number): Observable<HeroPower[]> {
    return this.http
      .get<HeroPower[]>(`${this._url}/${heroId}`)
      .pipe(catchError(this.handleError<HeroPower[]>('getHeroPowers', [])));
  }

  public trainPower(heroPowerId: number): Observable<any> {
    return this.http
      .get<any>(`${this._url}/train/${heroPowerId}`)
      .pipe(catchError(this.handleError<any>('trainPowers', [])));
  }

  public changeMainPower(mainPowerDto: MainPowerChange): Observable<any> {
    return this.http
      .post(`${this._url}`, mainPowerDto)
      .pipe(catchError(this.handleError<any>('changeMainPower', [])));
  }

  public deletePower(id: number): Observable<any> {
    return this.http
      .delete<any>(`${this._url}/${id}`)
      .pipe(catchError(this.handleError<any>('deleteHeroPower', [])));
  }

  public addVillainPowers(villainPowers: VillainPowers): Observable<any> {
    return this.http
      .post<any>(`${this._url}/powers`, villainPowers)
      .pipe(catchError(this.handleError<any>('addVillainPowers', [])));
  }

  public getPower(id: number): Observable<any> {
    return this.http
      .get<any>(`${this._url}/power/${id}`)
      .pipe(catchError(this.handleError<any>('getHeroPower', [])));
  }

  public updatePower(heroPower: HeroPower): Observable<any> {
    return this.http
      .put<any>(`${this._url}/update`, heroPower)
      .pipe(catchError(this.handleError<any>('updateHeroPower', [])));
  }
}
