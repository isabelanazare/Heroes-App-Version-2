import { ChangeLocationRendererComponent } from './../renderers/change-location-renderer/change-location-renderer.component';
import { Constants } from '../../utils/constants';
import { Component, OnInit } from '@angular/core';
import { HeroService } from '../../service/hero.service';
import { DataManagementBase } from '../../utils/select-row';
import { BsModalService } from 'ngx-bootstrap/modal';
import { _ } from 'ag-grid-community';
import { HeroPictureRendererComponent } from '../renderers/hero-picture-renderer/hero-picture-renderer.component';
import { DeleteHeroButtonRendererComponent } from '../renderers/delete-hero-button-renderer/delete-hero-button-renderer.component';
import * as moment from 'moment';
import { DatePickerRendererComponent } from '../renderers/date-picker-renderer/date-picker-renderer.component';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/service/alert.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.scss'],
})
export class HeroesComponent extends DataManagementBase implements OnInit {
  public columnDefs = [
    {
      headerName: 'Profile Picture',
      field: 'avatarPath',
      cellRenderer: 'heroPictureRenderer',
      width: 175,
      cellRendererParams: {
        onClick: this._onUploadAvatar.bind(this),
      },
    },
    {
      headerName: 'Name',
      field: 'name',
      sortable: true,
      filter: true,
      width: 150,
    },
    {
      headerName: 'Birthday',
      field: 'birthday',
      sortable: true,
      filter: true,
      width: 175,
      editable: true,
      cellEditor: 'datePicker',
    },
    {
      headerName: 'Main Power',
      field: 'mainPower',
      sortable: true,
      filter: true,
      width: 200,
    },
    {
      headerName: 'Ally',
      field: 'ally',
      sortable: true,
      filter: true,
      width: 200,
    },
    {
      headerName: 'Other Powers',
      field: 'otherPowers',
      sortable: true,
      filter: true,
      width: 250,
    },
    {
      headerName: 'Overall Strength',
      field: 'overallStrength',
      sortable: true,
      filter: true,
      width: 150,
    },
    {
      headerName: 'Location',
      field: 'location',
      cellRenderer: 'changeLocationRenderer',
      width: 100,
      cellRendererParams: {
        onClick: this._onDelete.bind(this),
      },
    },
    {
      headerName: 'Delete',
      field: 'delete',
      cellRenderer: 'deleteHeroRenderer',
      width: 100,
      cellRendererParams: {
        onClick: this._onDelete.bind(this),
      },
    },
  ];
  public rowHeight = 70;
  public rowData: any;
  public rowClassRules = {
    'birthday-warning': function (params) {
      const now = moment();
      const sunday = now.clone().weekday(7);
      const nextSunday = sunday.clone().weekday(7);
      const birthday = moment(new Date(params.data.birthday));
      const isHeroBirthdayNextWeek =
        birthday <= nextSunday && birthday > sunday;

      return isHeroBirthdayNextWeek;
    },

  };

  public frameworkComponents = {
    heroPictureRenderer: HeroPictureRendererComponent,
    deleteHeroRenderer: DeleteHeroButtonRendererComponent,
    datePicker: DatePickerRendererComponent,
    changeLocationRenderer: ChangeLocationRendererComponent,
  };

  constructor(
    private alertService: AlertService,
    protected router: Router,
    private heroService: HeroService,
    protected modalService: BsModalService
  ) {
    super(modalService, router);
  }

  public getRowStyle() {}

  public ngOnInit() {
    this._loadData();
  }

  private _onUploadAvatar() {
    this._loadData();
  }

  private _onDelete() {
    this._loadData();
  }

  public showEditModal(editModal) {
    if(this.selectedDataId != undefined){
      this.showModal(editModal);
    }
    else {
      this.alertService.alertError("You have to select a hero first");
    }
  }

  protected _loadData() {
    this.heroService.getHeroesRowData().subscribe((heroes) => {
      this.rowData = heroes;
    });
  }
}
