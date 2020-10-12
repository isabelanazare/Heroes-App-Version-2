import { HeroService } from 'src/app/service/hero.service';
import { ChildRendererBase } from 'src/app/utils/ChildRendererBase';
import { Component } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-location-renderer',
  templateUrl: './change-location-renderer.component.html',
  styleUrls: ['./change-location-renderer.component.css'],
})
export class ChangeLocationRendererComponent extends ChildRendererBase {
  constructor(
    protected router: Router,
    protected HeroService: HeroService,
    protected modalService: BsModalService
  ) {
    super(router, modalService);
  }
  public edit() {}

  ngOnInit(): void {}

  protected _loadData() {}
}
