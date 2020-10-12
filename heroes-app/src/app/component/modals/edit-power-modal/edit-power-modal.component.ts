import { Component, OnInit, Input } from '@angular/core';
import { PowerModalBase } from 'src/app/modals/power-modal-helper';
import { PowerService } from 'src/app/service/power.service';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { ElementService } from 'src/app/service/element.service';
import { Constants } from 'src/app/utils/constants';
import { Power } from 'src/app/models/power';
import { forkJoin } from 'rxjs';
import { ElementType } from 'src/app/models/element-type';

@Component({
  selector: 'app-edit-power-modal',
  templateUrl: './edit-power-modal.component.html',
  styleUrls: ['./edit-power-modal.component.css'],
})
export class EditPowerModalComponent extends PowerModalBase implements OnInit {
  @Input('selectedDataId') powerId: number;
  @Input('selectedDataName') powerName: string;
  public currenElement: ElementType = new ElementType();

  constructor(
    private powerService: PowerService,
    private errorHandler: ErrorHandlerService,
    private elementService: ElementService
  ) {
    super();
  }

  public ngOnInit(): void {
    this._initModal();
  }

  public onSave() {
    this._editPower(this.powerId);
  }

  private _initModal() {
    forkJoin(
      this.elementService.getElements(),
      this.powerService.getPower(this.powerId)
    ).subscribe(([elements, power]) => {
      this.elements = elements;
      this.power = power;
      this.currenElement = new ElementType(power.elementId, power.element);
    });
  }

  private _editPower(powerId: number) {
    this.power.id = powerId;
    if (!this.errorHandler.checkPowerFields(this.power)) {
      this.errorHandler.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE + this.errorHandler.errorMessage
      );
      return;
    }
    this.powerService.updatePower(this.power).subscribe((_) => {
      this.close.emit(true);
      this.power = new Power();
    });
  }
}
