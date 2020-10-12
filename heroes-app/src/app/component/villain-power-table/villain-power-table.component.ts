import { AlertService } from './../../service/alert.service';
import { DeleteHeroPowerRendererComponent } from './../renderers/delete-hero-power-renderer/delete-hero-power-renderer.component';
import { HeroPowerService } from './../../service/hero-power.service';
import { Component, Input, OnInit } from '@angular/core';
import { DataManagementBase } from 'src/app/utils/select-row';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MainPowerCheckRendererComponent } from '../renderers/main-power-check-renderer/main-power-check-renderer.component';

@Component({
  selector: 'app-villain-power-table',
  templateUrl: './villain-power-table.component.html',
  styleUrls: ['./villain-power-table.component.css'],
})
export class VillainPowerTableComponent
  extends DataManagementBase
  implements OnInit {
  @Input() villainId: number;

  public rowData: any;

  public columnDefs = [
    {
      headerName: 'Name',
      field: 'name',
      sortable: true,
      filter: true,
      width: 250,
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
      width: 200,
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
      headerName: 'Strength',
      field: 'strength',
      sortable: true,
      filter: true,
      width: 150,
    },
    {
      headerName: 'Delete',
      field: 'delete',
      cellRenderer: 'deleteHeroPowerRenderer',
      width: 150,
      cellRendererParams: {
        onClick: this._loadData.bind(this),
      },
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

  public frameworkComponents = {
    deleteHeroPowerRenderer: DeleteHeroPowerRendererComponent,
    mainPowerRenderer: MainPowerCheckRendererComponent,
  };

  public rowHeight = 70;

  constructor(
    private heroPowerService: HeroPowerService,
    protected router: Router,
    protected modalService: BsModalService,
    private alertService: AlertService
  ) {
    super(modalService, router);
  }

  ngOnInit(): void {
    this._loadData();
  }

  protected _loadData(): void {
    this.heroPowerService
      .getHeroPowersForHero(this.villainId)
      .subscribe((powers) => (this.rowData = powers));
  }

  public showEditModal(editModal) {
    if (this.selectedDataId != undefined) {
      this.showModal(editModal);
    } else {
      this.alertService.alertError('You have to select a power first');
    }
  }
}
