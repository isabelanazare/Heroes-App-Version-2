import { Component, OnInit } from '@angular/core';
import { PowerService } from 'src/app/service/power.service';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { PowerModalBase } from '../../../modals/power-modal-helper';
import { ElementService } from '../../../service/element.service';
import { Constants } from 'src/app/utils/constants';
import { Power } from 'src/app/models/power';

@Component({
  selector: 'app-add-power-modal',
  templateUrl: './add-power-modal.component.html',
  styleUrls: ['./add-power-modal.component.css'],
})
export class AddPowerModalComponent extends PowerModalBase implements OnInit {
  constructor(
    private powerService: PowerService,
    private errorHandler: ErrorHandlerService,
    private elementService: ElementService
  ) {
    super();
  }

  public ngOnInit(): void {
    this._getElemnts();
  }

  public onSave() {
    this._savePower();
  }

  private _savePower() {
    if (!this.errorHandler.checkPowerFields(this.power)) {
      this.errorHandler.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE + this.errorHandler.errorMessage
      );
      return;
    }
    this.powerService.savePower(this.power).subscribe((_) => {
      this.close.emit(true);
      this.power = new Power();
    });
  }

  private _getElemnts() {
    this.elementService.getElements().subscribe((elements) => {
      this.elements = elements;
    });
  }
}
