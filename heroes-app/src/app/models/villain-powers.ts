import { Power } from './power';
export class VillainPowers {
  public villainId: number;
  public powerIds: number[];

  constructor(id: number, powers: number[]) {
    this.villainId = id;
    this.powerIds = powers;
  }
}
