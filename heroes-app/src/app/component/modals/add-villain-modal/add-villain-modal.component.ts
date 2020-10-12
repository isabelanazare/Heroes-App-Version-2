import { Component, OnInit } from '@angular/core';
import { HeroTypeService } from '../../../service/hero-type.service';
import { Constants } from 'src/app/utils/constants';
import { PowerService } from 'src/app/service/power.service';
import { VillainService } from 'src/app/service/villain.service';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { VillainModalHelper } from '../../../modals/villain-modal-helper';
import { forkJoin } from 'rxjs';
import { Villain } from 'src/app/models/villain';
import { Power } from 'src/app/models/power';

@Component({
  selector: 'app-add-villain-modal',
  templateUrl: './add-villain-modal.component.html',
  styleUrls: ['./add-villain-modal.component.scss']
})
export class AddVillainModalComponent extends VillainModalHelper implements OnInit {
  constructor(
    private heroTypeService: HeroTypeService,
    private powerService: PowerService,
    private villainService: VillainService,
    private errorHandler: ErrorHandlerService
  ) {
    super();
  }

  public ngOnInit() {
    forkJoin([
      this.heroTypeService.getTypes(),
      this.powerService.getPowers(),
      this.villainService.getVillains(),
    ]).subscribe(([types, powers, allies]) => {
      this.heroTypes = types;
      this.powers = powers;
      this.allies = allies;
      this.villain.mainPower = new Power(0);
    });
  }

  public onSave() {
    this.convertPowers(this.villain);
    this.convertAllies(this.villain);
    this._checkAndSubscribeVillain();
  }

  private _checkAndSubscribeVillain() {
    if (!this.errorHandler.checkVillainFields(this.villain)) {
      this.errorHandler.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE + this.errorHandler.errorMessage
      );
    } else {
      this.villainService.addVillain(this.villain).subscribe((_) => {
        this.close.emit(true);
        this.villain = new Villain();
      });
    }
  }
}
