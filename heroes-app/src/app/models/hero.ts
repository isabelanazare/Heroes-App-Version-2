import { Power } from './power';

export class Hero {
  public id: number;
  public name: string;
  public powers: Power[];
  public type: string;
  public allies: Hero[];
  public typeId: number;
  public avatarPath: string;
  public birthday: string;
  public mainPower: Power;
  public latitude = 46.778;
  public longitude = 23.607;
  public email: string;
  public role: string;
  public overallStrength: number;
  public isGod: boolean;
  public isBadGuy: boolean;

  constructor(id?: number) {
    this.id = id;
  }
}
