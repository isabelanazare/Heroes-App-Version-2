import { VillainPowers } from './../../../models/villain-powers';
import { VillainService } from './../../../service/villain.service';
import { forkJoin } from 'rxjs';
import { HeroPowerService } from './../../../service/hero-power.service';
import { PowerModalBase } from 'src/app/modals/power-modal-helper';
import { Component, Input, OnInit } from '@angular/core';
import { PowerService } from 'src/app/service/power.service';
import { Power } from 'src/app/models/power';

@Component({
  selector: 'app-villain-add-powers',
  templateUrl: './villain-add-powers.component.html',
  styleUrls: ['./villain-add-powers.component.css'],
})
export class VillainAddPowersComponent
  extends PowerModalBase
  implements OnInit {
  @Input() villainId: number;

  public powers: Power[] = [];
  public selectedPowers: Power[] = [];
  public mainPower: Power;

  constructor(
    private powerService: PowerService,
    private heroPowerService: HeroPowerService,
    private villainService: VillainService
  ) {
    super();
  }

  ngOnInit(): void {
    this._initModal();
  }

  private _initModal() {
    forkJoin([
      this.powerService.getPowers(),
      this.villainService.getVillainById(this.villainId),
    ]).subscribe(([powers, villain]) => {
      let initialPowers = powers;
      initialPowers.forEach((power) => {
        let ok = true;
        if (villain.mainPower) {
          if (power.id === villain.mainPower.id) {
            ok = false;
          }
        }
        villain.powers.forEach((villainPower) => {
          if (power.id === villainPower.id) {
            ok = false;
          }
        });
        if (ok) {
          this.powers.push(power);
        }
      });
    });
  }

  public onSave() {
    const villainPowers: VillainPowers = new VillainPowers(
      this.villainId,
      this.extractPowerIds()
    );
    this.heroPowerService.addVillainPowers(villainPowers).subscribe(() => {
      this.close.emit(true);
    });
  }

  private extractPowerIds(): number[] {
    let powerIds: number[] = [];
    this.selectedPowers.forEach((power) => powerIds.push(power.id));
    return powerIds;
  }

  public isSelected(power: Power): boolean {
    return this.selectedPowers.findIndex((item) => item.id === power.id) > -1
      ? true
      : false;
  }

  public selectSuggestion(power: Power): void {
    this.selectedPowers.find((item) => item.id === power.id)
      ? (this.selectedPowers = this.selectedPowers.filter(
          (item) => item.id !== power.id
        ))
      : this.selectedPowers.push(power);
  }

  public deleteSelects(power: Power): void {
    this.selectedPowers = this.selectedPowers.filter(
      (item) => item.id !== power.id
    );
  }
}
