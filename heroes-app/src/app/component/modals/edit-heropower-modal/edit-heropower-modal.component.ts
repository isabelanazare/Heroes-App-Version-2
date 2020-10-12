import { HeroPower } from './../../../models/hero-power';
import { HeroPowerService } from './../../../service/hero-power.service';
import { Component, Input, OnInit } from '@angular/core';
import { PowerModalBase } from 'src/app/modals/power-modal-helper';
import { ElementService } from 'src/app/service/element.service';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { PowerService } from 'src/app/service/power.service';
import { forkJoin } from 'rxjs';
import { ElementType } from 'src/app/models/element-type';

@Component({
  selector: 'app-edit-heropower-modal',
  templateUrl: './edit-heropower-modal.component.html',
  styleUrls: ['./edit-heropower-modal.component.css'],
})
export class EditHeropowerModalComponent
  extends PowerModalBase
  implements OnInit {
  @Input('selectedDataId') powerId: number;
  @Input('selectedDataName') powerName: string;
  public power: HeroPower;

  public currentElement: ElementType;

  constructor(
    private powerService: PowerService,
    private errorHandler: ErrorHandlerService,
    private elementService: ElementService,
    private heroPowerService: HeroPowerService
  ) {
    super();
  }

  ngOnInit(): void {
    this._initModal();
  }

  private _initModal() {
    forkJoin(
      this.elementService.getElements(),
      this.heroPowerService.getPower(this.powerId)
    ).subscribe(([elements, power]) => {
      this.elements = elements;
      this.power = power;
      this.currentElement = new ElementType(power.elementId, power.element);
      this.elements = this.elements.filter(
        (element) => element.id != this.currentElement.id
      );
    });
  }

  public onSave() {
    this.heroPowerService.updatePower(this.power).subscribe((_) => {
      this.close.emit(true);
    });
  }
}
