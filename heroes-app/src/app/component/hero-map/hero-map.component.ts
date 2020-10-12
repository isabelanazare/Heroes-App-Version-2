import { catchError } from 'rxjs/operators';
import {
  ChangeDetectorRef,
  Component,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { EntityTypeDto } from 'src/app/models/entity-type-dto';
import { Hero } from 'src/app/models/hero';
import { HeroService } from 'src/app/service/hero.service';
import { BattleService } from 'src/app/service/battle.service';
import { Constants } from '../../utils/constants';
import { BsModalRef, ModalDirective } from 'ngx-bootstrap/modal';
import { Villain } from 'src/app/models/villain';
import { BattleResult } from 'src/app/models/battle-result';
import { AlertService } from 'src/app/service/alert.service';
import { Battle } from 'src/app/models/battle';

@Component({
  selector: 'app-hero-map',
  templateUrl: './hero-map.component.html',
  styleUrls: ['./hero-map.component.scss'],
})
export class HeroMapComponent implements OnInit {
  openedWindow;
  heroes: any[];
  loggedHero: EntityTypeDto;
  loggedHeroId: number;
  latitude: number = 51.673858;
  longitude: number = 7.815982;
  zoom: number = 8;
  travelDistance: number;
  clickCounter: number = 0;
  icon = {
    url: 'assets/svg/placeholder_hero.svg',
    scaledSize: {
      width: 40,
      height: 50,
    },
  };

  iconHero = {
    url: 'assets/svg/placeholder_current_hero.svg',
    scaledSize: {
      width: 40,
      height: 50,
    },
  };

  iconVillain = {
    url: 'assets/svg/placeholder_villain.svg',
    scaledSize: {
      width: 40,
      height: 50,
    },
  };

  iconTravel = {
    url: 'assets/svg/placeholder_travel.svg',
    scaledSize: {
      width: 40,
      height: 50,
    },
  };

  public modalRef: BsModalRef;

  @Input() mapToggle: boolean;
  constructor(
    private alertService: AlertService,
    protected _heroService: HeroService,
    protected router: Router,
    private battleService: BattleService,
    private cdr: ChangeDetectorRef
  ) {}

  public heroesBattle: EntityTypeDto[] = [];
  public villainsBattle: EntityTypeDto[] = [];

  public heroesTeam: Hero[] = [];
  public villainsTeam: Villain[] = [];

  public battleResult: BattleResult;
  public currentUser;
  public battle: Battle;

  public heroesScore: number;
  public villainsScore: number;

  public chances: number;

  public showModal: boolean = true;
  @ViewChild('createLobbyBattle') lobbyModal: ModalDirective;
  @Input() hero: Hero;
  @ViewChild('agm') agm: any;

  ngOnInit() {
    this.loggedHeroId = this.hero.id;
    this.loggedHero = new EntityTypeDto(
      this.loggedHeroId,
      this.hero.role.toLowerCase() === 'villain',
      this.hero.name
    );
    this.loggedHero.latitude = this.hero.latitude;
    this.loggedHero.longitude = this.hero.longitude;

    this._heroService.getEntityTypeDto(this.loggedHeroId).subscribe((hero) => {
      this.loggedHero = hero;
    });
    this.latitude = this.hero.latitude;
    this.longitude = this.hero.longitude;
    this._heroService.getAllPlayers().subscribe((heroes) => {
      this.heroes = heroes;
    });
  }

  public displayAffiliation(hero: any): string {
    return hero.isBadGuy ? Constants.VILLAIN : Constants.HERO;
  }

  public seeLocationHeroOrVillain(data: Hero): void {
    this.latitude = data.latitude;
    this.longitude = data.longitude;
    this.zoom = 15;
  }

  public isInfoWindowOpen(id): boolean {
    return this.openedWindow == id;
  }

  public closeWindow(): void {
    this.openedWindow = null;
  }

  public openWindow(data): void {
    this.latitude = data.address.latitude;
    this.longitude = data.address.longitude;
    this.zoom = 15;
    setTimeout(() => {
      this.openedWindow = data.id;
    }, 100);
  }

  public openWindowFromSideMenu(data): void {
    this.latitude = data.latitude;
    this.longitude = data.longitude;
    this.zoom = 15;
    setTimeout(() => {
      this.openedWindow = data.id;
    }, 100);
  }

  public placeMarker($event): void {
    this.clickCounter++;
    const RADIANS: number = 180 / Constants.PIE;

    if (
      this.loggedHero.latitude == $event.coords.lat &&
      this.loggedHero.longitude == $event.coords.lng
    ) {
      this.travelDistance = 0;
    } else {
      // Calculating Distance between Points
      var lt1 = this.loggedHero.latitude / RADIANS;
      var lg1 = this.loggedHero.longitude / RADIANS;
      var lt2 = $event.coords.lat / RADIANS;
      var lg2 = $event.coords.lng / RADIANS;

      // radius of earth in miles (3,958.8) * metres in a mile * position on surface of sphere...
      this.travelDistance =
        6371 *
        1000 *
        Math.acos(
          Math.sin(lt1) * Math.sin(lt2) +
            Math.cos(lt1) * Math.cos(lt2) * Math.cos(lg2 - lg1)
        );
      this.latitude = $event.coords.lat;
      this.longitude = $event.coords.lng;
      this.zoom = 12;
    }
  }

  private _checkLevelCompatibility(initiator, opponent): boolean {
    if (!initiator.isGod && !opponent.isGod) {
      return true;
    }
    return initiator.isGod && opponent.isGod;
  }

  public createBattle(opponent) {
    if (this._checkLevelCompatibility(this.hero, opponent)) {
      this.battleService.createBattle(this.hero.id, opponent.id).subscribe(
        (battle) => {
          this.heroesTeam = battle.heroes;
          this.heroesScore = battle.heroesStrength;
          this.villainsTeam = battle.villains;
          this.villainsScore = battle.villainsStrength;
          this.battle = battle;

          if (!this.loggedHero.isBadGuy) {
            this.chances = this._computeChances(
              this.heroesScore,
              this.villainsScore
            );
          } else {
            this.chances = this._computeChances(
              this.villainsScore,
              this.heroesScore
            );
          }

          if (battle.initiatorId === this.loggedHeroId) {
            this.lobbyModal.show();
          }
        },
        (err) => this.lobbyModal.hide()
      );
    } else {
      this.lobbyModal.hide();
      this.alertService.alertError(Constants.INCOMPATIBLE_LEVELS);
    }
  }

  public playGame() {
    this.lobbyModal.hide();
    this.alertService.alertBattleStarted();
    this.battleService.playGame(this.battle).subscribe((res) => {
      this.battleResult = res;
      let currentNrOfBattles: number = +localStorage.getItem('battleCount');
      localStorage.setItem('battleCount', (currentNrOfBattles + 1).toString());
      this.alertService.alertBattleFinished().then(() => {
        this._seeResult();
      });
    });
  }

  private _seeResult() {
    const finalScore = `Final score<br>Heroes Overall strength: ${this.battleResult.heroesStrength}<br>Villains Overall strength: ${this.battleResult.villainStrength}`;
    if (this.battleResult.result == "It's a tie") {
      this.alertService.alertEqualScore(this.battleResult.result, finalScore);
    } else if (
      (this.battleResult.result === 'Heroes won' &&
        this.hero.role.toLowerCase() === 'hero') ||
      (this.battleResult.result === 'Villains won' &&
        this.hero.role.toLowerCase() === 'villain')
    ) {
      this.alertService.alertWon('You won', finalScore);
    } else {
      this.alertService.alertLost('You lost', finalScore);
    }
    this._heroService.getEntityTypeDto(this.loggedHeroId).subscribe((hero) => {
      this.loggedHero = hero;
      this._heroService.getAllPlayers().subscribe((heroes) => {
        this.heroes = heroes;
        this.cdr.detectChanges();
      });
    });
  }

  public cancelBattle() {
    this.battleService.deleteBattle(this.battle.id).subscribe();
  }

  public showBattleResult(battleResult) {
    if (battleResult.villainsStregth > battleResult.heroesStrength) {
      if (this.loggedHero.isBadGuy) {
        this.alertService.alertWon(
          Constants.YOU_WON_MESSAGE,
          `${Constants.HEROES_SCORE} ${battleResult.heroesStrength}, ${Constants.VILLAINS_SCORE} ${battleResult.villainsStregth}`
        );
      } else {
        this.alertService.alertLost(
          Constants.YOU_LOST_MESSAGE,
          `${Constants.HEROES_SCORE} ${battleResult.heroesStrength}, ${Constants.VILLAINS_SCORE} ${battleResult.villainsStregth}`
        );
      }
    } else if (battleResult.villainsStregth === battleResult.heroesStrength) {
      this.alertService.alertEqualScore(
        Constants.EQUAL_SCORE_MESSAGE,
        `${Constants.HEROES_SCORE} ${battleResult.heroesStrength}, ${Constants.VILLAINS_SCORE} ${battleResult.villainsStregth}`
      );
    } else {
      if (!this.loggedHero.isBadGuy) {
        this.alertService.alertWon(
          Constants.YOU_WON_MESSAGE,
          `${Constants.HEROES_SCORE} ${battleResult.heroesStrength}, ${Constants.VILLAINS_SCORE} ${battleResult.villainsStregth}`
        );
      } else {
        this.alertService.alertLost(
          Constants.YOU_LOST_MESSAGE,
          `${Constants.HEROES_SCORE} ${battleResult.heroesStrength}, ${Constants.VILLAINS_SCORE} ${battleResult.villainsStregth}`
        );
      }
    }
  }

  public travel(): void {
    this._heroService
      .changeHeroTravel(this.loggedHeroId, this.latitude, this.longitude)
      .subscribe((result) => {
        this.hero = result;
        this._heroService
          .getEntityTypeDto(this.loggedHeroId)
          .subscribe((hero) => {
            this.loggedHero = hero;
          });
        this.clickCounter = 0;
        this.closeWindow();
        this._heroService.getAllPlayers().subscribe((heroes) => {
          this.heroes = heroes;
          this.cdr.detectChanges();
        });
        this.cdr.detectChanges();
      });
  }

  private _computeChances(score1, score2): number {
    if (score1 === score2) {
      return 50;
    }
    if (score1 > score2) {
      return 75;
    } else {
      return 25;
    }
  }
}
