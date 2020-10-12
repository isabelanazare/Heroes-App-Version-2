import { HeroType } from '../models/hero-type';
import { Power } from '../models/power';
import { Villain } from '../models/villain';
import { Constants } from '../utils/constants';
import { ModalBase } from './modal-base';

export abstract class VillainModalHelper extends ModalBase {
  public heroTypes: HeroType[] = [];
  public powers: Power[] = [];

  public localFieldsPowers: any = { text: 'name', value: 'id' };
  public localFieldsAllies: any = { text: 'name', value: 'id' };

  public placeholderPowers: string = Constants.POWERS_MULTISELECT_PLACEHOLDER;
  public placeholderAllies: string = Constants.ALLIES_MULTISELECT_PLACEHLODER;

  public villain: Villain = new Villain();
  public allies: Villain[] = [];
  public powersIds: number[] = [];
  public alliesIds: number[] = [];

  public selectionHint: string = Constants.HERO_SELECTION_HINT;

  public convertPowers(villain: Villain) {
    if (this.powersIds.length === 0) {
      this.villain.mainPower = new Power();
      this.villain.powers = [];
      return;
    }
    this.powersIds = this.powersIds?.filter(
      (powerId) => +powerId !== +this.villain.mainPower.id
    );
    villain.powers = this.powersIds?.map((powerId) => {
      return new Power(powerId);
    });
  }

  public convertAllies(villain: Villain) {
    villain.allies = this.alliesIds?.map((allyId) => {
      return new Villain(allyId);
    });
  }

  public getSelectedType(selectedValue: any) {
    this.villain.typeId = selectedValue.target.value;
  }

  public getPowerWithId(id: number): Power {
    return this.powers.find((p) => p.id === id);
  }

  public getSelectedMainPower(selectedValue: any) {
    this.villain.mainPower = new Power(selectedValue.target.value);
  }
}
