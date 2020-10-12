import { ElementType } from '../models/element-type';
import { Constants } from '../utils/constants';
import { Power } from '../models/power';
import { ModalBase } from './modal-base';

export abstract class PowerModalBase extends ModalBase {
  public power: Power = new Power();
  public elementSelectionHint: string = Constants.ELEMENT_SELECTION_HINT;
  public elements: ElementType[] = [];

  public getSelectedElement(selectedValue: any) {
    this.power.elementId = selectedValue.target.value;
  }
}
