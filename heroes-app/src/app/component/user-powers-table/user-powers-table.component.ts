import { MainPowerCheckRendererComponent } from './../renderers/main-power-check-renderer/main-power-check-renderer.component';
import { HeroPowerService } from './../../service/hero-power.service';
import { TrainingButtonRendererComponent } from './../renderers/training-button-renderer/training-button-renderer.component';
import { DataManagementBase } from 'src/app/utils/select-row';
import { Hero } from 'src/app/models/hero';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PowerService } from 'src/app/service/power.service';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-user-powers-table',
  templateUrl: './user-powers-table.component.html',
  styleUrls: ['./user-powers-table.component.css'],
})
export class UserPowersTableComponent
  extends DataManagementBase
  implements OnInit {
  @Input() hero: Hero;

  public columnDefs = [
    {
      headerName: 'Name',
      field: 'name',
      sortable: true,
      filter: true,
      width: 300,
    },
    {
      headerName: 'Main Trait',
      field: 'mainTrait',
      sortable: true,
      filter: true,
      width: 280,
    },
    {
      headerName: 'Element',
      field: 'element',
      sortable: true,
      filter: true,
      width: 150,
    },
    {
      headerName: 'Strength',
      field: 'strength',
      sortable: true,
      filter: true,
      width: 150,
    },
    {
      headerName: 'Details',
      field: 'details',
      sortable: true,
      filter: true,
      width: 400,
      cellStyle: {
        'white-space': 'break-spaces',
        'line-height': '1.6',
        align: 'center',
      },
    },
    {
      headerName: 'Train',
      field: 'train',
      cellRenderer: 'trainPowerRenderer',
      cellRendererParams: {
        onClick: this._loadData.bind(this),
      },
      width: 150,
    },
    {
      headerName: 'Main Power',
      field: 'mainPower',
      cellRenderer: 'mainPowerRenderer',
      cellRendererParams: {
        onClick: this._loadData.bind(this),
      },
      width: 150,
    },
  ];
  public rowHeight = 70;
  public rowData: any;

  public frameworkComponents = {
    trainPowerRenderer: TrainingButtonRendererComponent,
    mainPowerRenderer: MainPowerCheckRendererComponent,
  };

  constructor(
    protected router: Router,
    private powersService: PowerService,
    private heroPowersService: HeroPowerService,
    protected modalService: BsModalService
  ) {
    super(modalService, router);
  }

  ngOnInit(): void {
    this._loadData();
  }

  protected _loadData() {
    this.heroPowersService
      .getHeroPowersForHero(this.hero.id)
      .subscribe((result) => {
        this.rowData = result;
      });
  }
}
