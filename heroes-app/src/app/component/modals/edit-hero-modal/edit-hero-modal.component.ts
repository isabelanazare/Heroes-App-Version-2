import { Component, OnInit, Input } from '@angular/core';
import { HeroModalHelper } from 'src/app/modals/hero-modal-helper';
import { HeroTypeService } from 'src/app/service/hero-type.service';
import { PowerService } from 'src/app/service/power.service';
import { HeroService } from 'src/app/service/hero.service';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { forkJoin } from 'rxjs';
import { Constants } from 'src/app/utils/constants';
import { Hero } from 'src/app/models/hero';
import { HeroType } from 'src/app/models/hero-type';
import { Power } from 'src/app/models/power';

@Component({
  selector: 'app-edit-hero-modal',
  templateUrl: './edit-hero-modal.component.html',
  styleUrls: ['./edit-hero-modal.component.css'],
})
export class EditHeroModalComponent extends HeroModalHelper implements OnInit {
  @Input('selectedDataId') heroId: number;
  @Input('selectedDataName') heroName: string;
  public initialHeroType: HeroType = new HeroType();
  public initialMainPower: Power = new Power(0);

  constructor(
    private heroTypeService: HeroTypeService,
    private powerService: PowerService,
    private heroService: HeroService,
    private errorHandler: ErrorHandlerService
  ) {
    super();
  }

  public ngOnInit(): void {
    this._initModal();
  }

  public onSave() {
    this._editHero(this.heroId);
  }

  private _initModal() {
    this.startLoading();

    forkJoin([
      this.heroTypeService.getTypes(),
      this.powerService.getPowers(),
      this.heroService.getHeroes(),
      this.heroService.getHero(this.heroId),
    ]).subscribe(([types, powers, allies, hero]) => {
      this.heroTypes = types;
      this.powers = powers;
      this.allies = allies;
      this.hero = hero;
      this.initialHeroType = new HeroType(hero.typeId, hero.type);

      this.powersIds = this.hero.powers?.map((power) => power.id);
      this.alliesIds = this.hero.allies?.map((ally) => ally.id);

      if (!this.hero.mainPower) {
        this.hero.mainPower = new Power(0);
      }

      this.heroTypes = this.heroTypes.filter(
        (type) => type.id != this.initialHeroType.id
      );

      this.initialMainPower = new Power(hero.mainPower.id);
      this.stopLoading();
    });
  }

  private _editHero(heroId: number) {
    this.convertPowers(this.hero);
    this.convertAllies(this.hero);
    this.hero.id = heroId;
    this._checkAndSubscribeHero();
  }

  private _checkAndSubscribeHero() {
    if (!this.errorHandler.checkHeroFields(this.hero)) {
      this.errorHandler.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE + this.errorHandler.errorMessage
      );
      return;
    } else {
      this.heroService.updateHero(this.hero).subscribe((_) => {
        this.close.emit(true);
        this.hero = new Hero();
      });
    }
  }
}
