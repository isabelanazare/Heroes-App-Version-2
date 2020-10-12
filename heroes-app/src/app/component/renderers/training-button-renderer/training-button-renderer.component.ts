import { Constants } from './../../../utils/constants';
import { AlertService } from './../../../service/alert.service';
import { HeroPowerService } from './../../../service/hero-power.service';
import { HeroPower } from './../../../models/hero-power';
import { BsModalService } from 'ngx-bootstrap/modal';
import { HeroService } from 'src/app/service/hero.service';
import { ChildRendererBase } from 'src/app/utils/ChildRendererBase';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Power } from 'src/app/models/power';
import * as moment from 'moment';

@Component({
  selector: 'app-training-button-renderer',
  templateUrl: './training-button-renderer.component.html',
  styleUrls: ['./training-button-renderer.component.css'],
})
export class TrainingButtonRendererComponent extends ChildRendererBase {
  public power: HeroPower;

  constructor(
    protected router: Router,
    protected heroService: HeroService,
    protected modalService: BsModalService,
    private heroPowerService: HeroPowerService,
    private alertService: AlertService
  ) {
    super(router, modalService);
  }
  public edit() {}

  ngOnInit(): void {
    this.getSelectedId();
    this.power = this.params.node.data;
  }

  protected _loadData() {}

  public trainPower() {
    this.alertService.alertLoading();
    this.heroPowerService.trainPower(this.selectedId).subscribe(() => {
      this.alertService.alertSuccess('Power trained');
      this.params.onClick(this.params);
    });
  }

  public checkTime() {
    let now = moment();
    let lastTraining = moment(this.power.lastTrained);
    let diff = moment.duration(now.diff(lastTraining));
    let minutes = diff.asMinutes();
    return minutes > Constants.TRAININGTIME;
  }
}
