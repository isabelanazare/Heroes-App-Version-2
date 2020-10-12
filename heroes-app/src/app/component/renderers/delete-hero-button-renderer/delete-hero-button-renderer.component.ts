import { Component } from '@angular/core';
import { DeleteRendererBase } from 'src/app/utils/delete-renderer-base';
import { HeroService } from 'src/app/service/hero.service';
import { PowerService } from 'src/app/service/power.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-hero-button-renderer',
  templateUrl: './delete-hero-button-renderer.component.html',
  styleUrls: ['./delete-hero-button-renderer.component.css'],
})
export class DeleteHeroButtonRendererComponent extends DeleteRendererBase {
  constructor(
    protected router: Router,
    protected heroService: HeroService,
    protected powerService: PowerService,
    protected modalService: BsModalService
  ) {
    super(router, heroService, powerService, modalService);
  }

  protected _loadData() {}

  protected _delete() {
    this.getSelectedId();

    this.heroService
      .deleteHero(this.selectedId)
      .subscribe(() => this.params.onClick(this.params));
  }
}
