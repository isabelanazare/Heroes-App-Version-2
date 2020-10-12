import { Power } from './power';

export class Villain {
  public id: number;
  public name: string;
  public powers: Power[];
  public type: string;
  public typeId: number;
  public avatarPath: string;
  public birthday: string;
  public mainPower: Power;
  public allies: Villain[];
  public latitude = 51.673858;
  public longitude = 7.815982;
  public email: string;
  public overallStrength: number;

  constructor(id?: number) {
    this.id = id;
  }
}
