import { Component, OnInit } from '@angular/core';
import { HeroTypeService } from '../../../service/hero-type.service';
import { Constants } from 'src/app/utils/constants';
import { PowerService } from 'src/app/service/power.service';
import { HeroService } from 'src/app/service/hero.service';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { HeroModalHelper } from '../../../modals/hero-modal-helper';
import { forkJoin } from 'rxjs';
import { Hero } from 'src/app/models/hero';
import { Power } from 'src/app/models/power';

@Component({
  selector: 'app-add-hero-modal',
  templateUrl: './add-hero-modal.component.html',
  styleUrls: ['./add-hero-modal.component.css'],
})
export class AddHeroModalComponent extends HeroModalHelper implements OnInit {
  constructor(
    private heroTypeService: HeroTypeService,
    private powerService: PowerService,
    private heroService: HeroService,
    private errorHandler: ErrorHandlerService
  ) {
    super();
  }

  public ngOnInit() {
    this._initModal();
  }

  public onSave() {
    this._saveHero();
  }

  private _saveHero() {
    this.convertPowers(this.hero);
    this.convertAllies(this.hero);
    this._checkAndSubscribeHero();
  }

  private _checkAndSubscribeHero() {
    if (!this.errorHandler.checkHeroFields(this.hero)) {
      this.errorHandler.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE + this.errorHandler.errorMessage
      );
      return;
    } else {
      this.heroService.saveHero(this.hero).subscribe((_) => {
        this.close.emit(true);
        this.hero = new Hero();
      });
    }
  }

  private _initModal() {
    forkJoin([
      this.heroTypeService.getTypes(),
      this.powerService.getPowers(),
      this.heroService.getHeroes(),
    ]).subscribe(([types, powers, allies]) => {
      this.heroTypes = types;
      this.powers = powers;
      this.allies = allies;
      this.hero.mainPower = new Power(0);
    });
  }
}
