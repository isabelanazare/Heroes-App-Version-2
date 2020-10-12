import { Component } from '@angular/core';
import { DeleteRendererBase } from 'src/app/utils/delete-renderer-base';
import { HeroService } from 'src/app/service/hero.service';
import { PowerService } from 'src/app/service/power.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { VillainService } from 'src/app/service/villain.service';

@Component({
  selector: 'delete-hero-cell',
  templateUrl: './delete-villain-renderer.component.html',
  styleUrls: ['./delete-villain-renderer.component.scss']
})
export class DeleteVillainRendererComponent extends DeleteRendererBase{
  constructor(
    protected router: Router,
    protected heroService: HeroService,
    protected powerService: PowerService,
    protected modalService: BsModalService,
    private villainService: VillainService
  ) {
    super(router, heroService, powerService, modalService);
  }

  protected _loadData() {}

  protected _delete() {
    this.getSelectedId();

    this.villainService
      .deleteVillain(this.selectedId)
      .subscribe(() => this.params.onClick(this.params));
  }
}

