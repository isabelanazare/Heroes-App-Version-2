import { HeroPowerService } from './../../../service/hero-power.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { PowerService } from './../../../service/power.service';
import { HeroService } from './../../../service/hero.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DeleteRendererBase } from 'src/app/utils/delete-renderer-base';

@Component({
  selector: 'app-delete-hero-power-renderer',
  templateUrl: './delete-hero-power-renderer.component.html',
  styleUrls: ['./delete-hero-power-renderer.component.css'],
})
export class DeleteHeroPowerRendererComponent extends DeleteRendererBase {
  constructor(
    protected router: Router,
    protected heroService: HeroService,
    protected powerService: PowerService,
    protected modalService: BsModalService,
    private heroPowerService: HeroPowerService
  ) {
    super(router, heroService, powerService, modalService);
  }

  protected _loadData() {}

  protected _delete() {
    this.getSelectedId();
    this.heroPowerService
      .deletePower(this.selectedId)
      .subscribe(() => this.params.onClick(this.params));
  }
}
