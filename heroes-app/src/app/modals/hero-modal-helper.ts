import { HeroType } from '../models/hero-type';
import { Power } from '../models/power';
import { Hero } from '../models/hero';
import { Constants } from '../utils/constants';
import { ModalBase } from './modal-base';

export abstract class HeroModalHelper extends ModalBase {
  public heroTypes: HeroType[] = [];
  public powers: Power[] = [];

  public localFieldsPowers: any = { text: 'name', value: 'id' };
  public localFieldsAllies: any = { text: 'name', value: 'id' };

  public placeholderPowers: string = Constants.POWERS_MULTISELECT_PLACEHOLDER;
  public placeholderAllies: string = Constants.ALLIES_MULTISELECT_PLACEHLODER;

  public hero: Hero = new Hero();
  public allies: Hero[] = [];
  public powersIds: number[] = [];
  public alliesIds: number[] = [];

  public typeSelectionHint: string = Constants.HERO_SELECTION_HINT;

  public convertPowers(hero: Hero) {
    if (this.powersIds.length == 0) {
      this.hero.mainPower = new Power(0);
      this.hero.powers = [];
      return;
    }
    this.powersIds = this.powersIds?.filter(
      (powerId) => +powerId !== +this.hero.mainPower.id
    );
    hero.powers = this.powersIds?.map((powerId) => {
      return new Power(powerId);
    });
  }

  public convertAllies(hero: Hero) {
    hero.allies = this.alliesIds?.map((allyId) => {
      return new Hero(allyId);
    });
  }

  public getSelectedType(selectedValue: any) {
    this.hero.typeId = selectedValue.target.value;
  }

  public getPowerWithId(id: number): Power {
    return this.powers.find(p => p.id === id);
  }

  public getPowerName(id: number): string {
    for (var power of this.powers) {
      if (power.id === id) {
        return power.name;
      }
    }
    return new Power().name;
  }

  public getSelectedMainPower(selectedValue: any) {
    this.hero.mainPower = new Power(selectedValue.target.value);
  }
}
