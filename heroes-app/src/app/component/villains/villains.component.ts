import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { VillainService } from '../../service/villain.service';
import { DataManagementBase } from '../../utils/select-row';
import { GridOptions, _ } from 'ag-grid-community';
import { VillainImageRendererComponent } from '../renderers/villain-image-renderer/villain-image-renderer.component';
import { AlertService } from 'src/app/service/alert.service';
import { DeleteVillainRendererComponent } from '../renderers/delete-villain-renderer/delete-villain-renderer.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Module, AllCommunityModules } from '@ag-grid-community/all-modules';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/service/shared.service';
import { Constants } from 'src/app/utils/constants';

@Component({
  selector: 'app-villains',
  templateUrl: './villains.component.html',
  styleUrls: ['./villains.component.scss']
})
export class VillainsComponent extends DataManagementBase implements OnInit {
  public rowSelection: string = 'single';
  public columnDefs = [
    {
      headerName: 'Profile Picture',
      field: 'avatarPath',
      cellRendererFramework: VillainImageRendererComponent,
      colId: 'params',
      cellRendererParams: {
        onClick: this._refreshGrid.bind(this),
      },
      width: 200,
    },
    {
      headerName: 'Name',
      field: 'name',
      sortable: true,
      filter: true,
      width: 200,
    },
    {
      headerName: 'Birthday',
      field: 'birthday',
      sortable: true,
      filter: true,
      width: 190,
      editable: true,
      cellEditor: 'datePicker',
    },
    {
      headerName: 'Main Power',
      field: 'mainPower',
      sortable: true,
      filter: true,
      width: 220,
    },
    {
      headerName: 'Ally',
      field: 'ally',
      sortable: true,
      filter: true,
      width: 150,
    },
    {
      headerName: 'Other Powers',
      field: 'otherPowers',
      sortable: true,
      filter: true,
      width: 270,
    },
    {
      headerName: 'Overall Strength',
      field: 'overallStrength',
      sortable: true,
      filter: true,
      width: 250,
    },
    {
      headerName: 'Delete',
      field: 'delete',
      width: 100,
      cellRendererFramework: DeleteVillainRendererComponent,
      cellRendererParams: {
        onClick: this._refreshGrid.bind(this),
      },
    },
  ];
  public rowHeight = 70;
  public rowData: any;

  public gridOptions: GridOptions;
  public modules: Module[] = AllCommunityModules;
  public redirectRoute = Constants.VILLAIN_DETAIL_REDIRECT_ROUTE;

  constructor(
    private sharedService: SharedService,
    protected router: Router,
    private alertService: AlertService,
    private villainService: VillainService,
    protected modalService: BsModalService,
    private cdr: ChangeDetectorRef
  ) {
    super(modalService, router);

    this.gridOptions = <GridOptions>{
      context: {
        componentParent: this,
      },
      rowHeight: 50,
    };
  }

  public ngOnInit() {
    this.sharedService.currentIsUpdated.subscribe(isUpdated => {
      this.isUpdateSelected = isUpdated;
    });
    
    this._loadData();
  }

  private _refreshGrid() {
    this._loadData();
  }

  protected _loadData() {
    this.villainService.getVillainsRowData().subscribe((villains) => {
      this.rowData = villains;
    });
  }

  public updateCurrentVillainId() {
    this.villainService.getVillainById(this.selectedDataId).subscribe((res) => {
      this.sharedService.changeCurrentVillain(res);
      this.redirectRoute = `${res.id}`;
    });
  }

  public delete() {
    if (this.selectedDataId) {
      this.alertService.alertConfirm().then((result) => {
        if (result.value) {
          this.deleteVillain(this.selectedDataId);
          this.alertService.alertSuccess('Villain deleted');
        }
      });
    } else {
      this.alertService.alertError('Please select a villain');
    }
  }

  public deleteVillain(id: number) {
    this.villainService.deleteVillain(id).subscribe(() => {
      this.alertService.alertSuccess('Villain deleted');
    });
  }

  public edit() {
    if (this.selectedDataId !== undefined) {
      this.redirectToVillainDetails(this.selectedDataId, this.selectedDataName);
    }
    else {
      this.alertService.alertError(Constants.SELECT_VILLAIN_FIRST_MESSAGE);
    }
  }
}
