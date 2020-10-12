import { Component, OnInit } from '@angular/core';
import { DataManagementBase } from 'src/app/utils/select-row';
import { PowerService } from '../../service/power.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { DeletePowerButtonRendererComponent } from '../renderers/delete-power-button-renderer/delete-power-button-renderer.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-powers',
  templateUrl: './powers.component.html',
  styleUrls: ['./powers.component.scss'],
})
export class PowersComponent extends DataManagementBase implements OnInit {
  public saveModal: BsModalRef;
  public editModal: BsModalRef;

  public rowSelection = 'single';

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
      width: 200,
    },
    {
      headerName: 'Strength',
      field: 'strength',
      sortable: true,
      filter: true,
      width: 200,
    },
    {
      headerName: 'Details',
      field: 'details',
      sortable: true,
      filter: true,
      width: 500,
      cellStyle: {
        'white-space': 'break-spaces',
        'line-height': '1.6',
        align: 'center',
      },
    },
    {
      headerName: 'Delete',
      field: 'delte',
      cellRenderer: 'deletePowerRenderer',
      width: 100,
      cellRendererParams: {
        onClick: this._onDelete.bind(this),
      },
    },
  ];
  public rowHeight = 70;
  public rowData: any;

  public frameworkComponents = {
    deletePowerRenderer: DeletePowerButtonRendererComponent,
  };

  constructor(
    protected router: Router,
    private powersService: PowerService,
    protected modalService: BsModalService
  ) {
    super(modalService, router);
  }

  public ngOnInit() {
    this._loadData();
  }

  private _onDelete() {
    this._loadData();
  }

  protected _loadData() {
    this.powersService.getPowersDataTable().subscribe((powers) => {
      this.rowData = powers;
    });
  }
}
