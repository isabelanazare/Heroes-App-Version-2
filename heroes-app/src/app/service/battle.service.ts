import { BadgesCount } from './../models/badges-count';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ErrorHandlerService } from './error-handler.service';
import { Battle } from '../models/battle';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class BattleService extends ErrorHandlerService {
  private _battleUrl = 'https://localhost:44324/api/battle';

  constructor(private http: HttpClient) {
    super();
  }

  public createBattle(heroId: number, opponentId: number): Observable<Battle> {
    return this.http
      .post<Battle>(`${this._battleUrl}/${heroId}/${opponentId}`, {})
      .pipe(catchError(this.handleError<any>('create Battle', [])));
  }

  public playGame(battle: Battle): Observable<any> {
    return this.http
      .post<Battle>(`${this._battleUrl}/play`, battle)
      .pipe(catchError(this.handleError<any>('play Battle', [])));
  }

  public getBattlesHistory(heroId: number): Observable<any> {
    return this.http
      .get<Battle>(`${this._battleUrl}/${heroId}/history`, {})
      .pipe(catchError(this.handleError<any>('get battle history', [])));
  }

  public getBattlesCount(heroId: number): Observable<number> {
    return this.http
      .get<number>(`${this._battleUrl}/count/${heroId}`, {})
      .pipe(catchError(this.handleError<any>('get battle history', [])));
  }

  public deleteBattle(id: number): Observable<any> {
    return this.http
      .delete<any>(`${this._battleUrl}/${id}`)
      .pipe(catchError(this.handleError<any>('delete battle', [])));
  }

  public getBadgesCount(heroId: number): Observable<BadgesCount> {
    return this.http
      .get<BadgesCount>(`${this._battleUrl}/badges/${heroId}`, {})
      .pipe(catchError(this.handleError<any>('get battle history', [])));
  }
}
