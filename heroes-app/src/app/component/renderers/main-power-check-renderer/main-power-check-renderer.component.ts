import { MainPowerChange } from './../../../models/main-power-change';
import { HeroPowerService } from './../../../service/hero-power.service';
import { ChildRendererBase } from 'src/app/utils/ChildRendererBase';
import { Component, OnInit } from '@angular/core';
import { HeroPower } from 'src/app/models/hero-power';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { HeroService } from 'src/app/service/hero.service';
import { AlertService } from 'src/app/service/alert.service';

@Component({
  selector: 'app-main-power-check-renderer',
  templateUrl: './main-power-check-renderer.component.html',
  styleUrls: ['./main-power-check-renderer.component.css'],
})
export class MainPowerCheckRendererComponent extends ChildRendererBase {
  public heroPower: HeroPower;
  public isMainPower: boolean;
  protected _loadData() {}

  constructor(
    protected router: Router,
    protected heroService: HeroService,
    protected modalService: BsModalService,
    private heroPowerService: HeroPowerService,
    private alertService: AlertService
  ) {
    super(router, modalService);
  }

  ngOnInit(): void {
    this.getSelectedId();
    this.heroPower = this.params.node.data;
    this.isMainPower = this.heroPower.isMainPower;
  }

  checkedHandler(event) {
    let checked = event.target.checked;
    let mainPowerChange: MainPowerChange = new MainPowerChange(
      this.heroPower.id,
      checked
    );
    this.heroPowerService.changeMainPower(mainPowerChange).subscribe(() => {
      this.alertService.alertSuccess('Main Power changed');
      this.params.onClick(this.params);
    });
  }
}
