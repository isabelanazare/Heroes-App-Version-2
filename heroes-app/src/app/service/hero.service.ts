import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Hero } from '../models/hero';
import { EntityTypeDto } from '../models/entity-type-dto';
import { from, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HeroRowData } from '../models/hero-row-data';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root',
})
export class HeroService extends ErrorHandlerService {
  private _heroesUrl = 'https://localhost:44324/api/Heroes';

  constructor(private http: HttpClient) {
    super();
  }

  public getHeroes(): Observable<Hero[]> {
    return this.http
      .get<Hero[]>(this._heroesUrl)
      .pipe(catchError(this.handleError<Hero[]>('getHeroes', [])));
  }

  public getHero(id: number): Observable<Hero> {
    return this.http
      .get<Hero>(`${this._heroesUrl}/${id}`)
      .pipe(catchError(this.handleError<any>()));
  }

  public getHeroByUserId(id: number): Observable<Hero> {
    return this.http
      .get<Hero>(`${this._heroesUrl}/user/${id}`)
      .pipe(catchError(this.handleError<any>()));
  }

  public getAllPlayers(): Observable<Hero[]> {
    return this.http
      .get<Hero[]>(`${this._heroesUrl}/getAllPlayers`)
      .pipe(catchError(this.handleError<any>('getAllPlayers', [])));
  }

  public getHeroesRowData(): Observable<HeroRowData[]> {
    return this.http
      .get<HeroRowData[]>(`${this._heroesUrl}/heroesRowData`)
      .pipe(catchError(this.handleError<any>('getHeroesRowData', [])));
  }

  public getAminMapRowData(): Observable<HeroRowData[]> {
    return this.http
      .get<HeroRowData[]>(`${this._heroesUrl}/adminMapRowData`)
      .pipe(catchError(this.handleError<any>('getAdminRowData', [])));
  }

  public getEntityTypeDto(id: number): Observable<EntityTypeDto> {
    return this.http
      .get<EntityTypeDto>(`${this._heroesUrl}/entityTypeDto/${id}`)
      .pipe(catchError(this.handleError<any>()));
  }

  public getHeroesUnder500Km(heroId: number): Observable<Hero[]> {
    return this.http
      .get<Hero[]>(`${this._heroesUrl}/heroesInRange/${heroId}`)
      .pipe(catchError(this.handleError<any>('getHeroesUnder500Km', [])));
  }

  public getHeroesInBattleRange(id: number): Observable<EntityTypeDto[]> {
    return this.http
      .get<EntityTypeDto[]>(`${this._heroesUrl}/battle/heroes/${id}`)
      .pipe(catchError(this.handleError<any>('getHeroesInBattleRange', [])));
  }

  public getVillainsInBattleRange(id: number): Observable<EntityTypeDto[]> {
    return this.http
      .get<EntityTypeDto[]>(`${this._heroesUrl}/battle/villains/${id}`)
      .pipe(catchError(this.handleError<any>('getVillainsInBattleRange', [])));
  }

  public deleteHero(id: number): Observable<any> {
    return this.http
      .delete<any>(`${this._heroesUrl}/${id}`)
      .pipe(catchError(this.handleError<any>('deleteHero', [])));
  }

  public saveHero(hero: Hero): Observable<Hero> {
    return this.http
      .post<Hero>(this._heroesUrl, hero)
      .pipe(catchError(this.handleError<any>()));
  }

  public updateHero(hero: Hero): Observable<Hero> {
    return this.http
      .put<Hero>(this._heroesUrl, hero)
      .pipe(catchError(this.handleError<any>()));
  }

  public changeDetails(hero: Hero): Observable<Hero> {
    return this.http
      .put<Hero>(`${this._heroesUrl}/details`, hero)
      .pipe(catchError(this.handleError<any>()));
  }

  public updateHeroBirthday(id: number, birthday: string): Observable<any> {
    return this.http
      .put<any>(`${this._heroesUrl}/birthday?id=${id}&birthday=${birthday}`, {})
      .pipe(catchError(this.handleError<any>()));
  }

  public changeHeroAvatar(id: number, avatarPath: string): Observable<any> {
    return this.http
      .put<any>(
        `${this._heroesUrl}/avatar?id=${id}&avatarPath=${avatarPath}`,
        {}
      )
      .pipe(catchError(this.handleError<any>()));
  }

  public changeHeroLocation(
    id: number,
    latitude: number,
    longitude: number
  ): Observable<any> {
    return this.http
      .put<any>(
        `${this._heroesUrl}/location?id=${id}&latitude=${latitude}&longitude=${longitude}`,
        {}
      )
      .pipe(catchError(this.handleError<any>()));
  }

  public changeHeroTravel(
    id: number,
    latitude: number,
    longitude: number
  ): Observable<Hero> {
    return this.http
      .put<Hero>(
        `${this._heroesUrl}/travel?id=${id}&latitude=${latitude}&longitude=${longitude}`,
        {}
      )
      .pipe(catchError(this.handleError<any>()));
  }
}
