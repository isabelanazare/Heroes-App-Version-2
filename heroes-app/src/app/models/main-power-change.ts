export class MainPowerChange {
  public heroPowerId: number;
  public isMainPower: boolean;
  constructor(id: number, isMain: boolean) {
    this.heroPowerId = id;
    this.isMainPower = isMain;
  }
}
