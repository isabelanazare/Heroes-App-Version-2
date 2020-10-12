import { HeroService } from 'src/app/service/hero.service';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Hero } from 'src/app/models/hero';
import { BattleRecord } from 'src/app/models/battle-record';
import { BattleService } from 'src/app/service/battle.service';
import { AgGridAngular } from 'ag-grid-angular';

@Component({
  selector: 'app-battle-history',
  templateUrl: './battle-history.component.html',
  styleUrls: ['./battle-history.component.css'],
})
export class BattleHistoryComponent implements OnInit {
  @Input() hero: Hero;
  @ViewChild('agGrid') agGrid: AgGridAngular;

  public columnDefs = [
    {
      headerName: 'Latitude',
      field: 'battle.latitude',
      sortable: true,
      filter: true,
      width: 250,
    },
    {
      headerName: 'Longitude',
      field: 'battle.longitude',
      sortable: true,
      filter: true,
      width: 250,
    },
    {
      headerName: 'Result',
      field: 'hasWon',
      sortable: true,
      filter: true,
      width: 750,
      valueFormatter: (params) => this._wonFormatter(params),
    },
  ];

  public rowClassRules = {
    'win-row': function (params) {
      return params.data.hasWon;
    },
  };

  public rowHeight = 80;
  public rowData: any;
  public records: BattleRecord[];

  constructor(
    private battleService: BattleService,
    private heroService: HeroService
  ) {}

  ngOnInit(): void {
    this.battleService
      .getBattlesHistory(this.hero.id)
      .subscribe((battleRecords) => {
        this.rowData = battleRecords;
      });
  }

  private _wonFormatter(params) {
    if (params.data.hasWon) {
      return 'Won';
    }
    return 'Lost/Tie';
  }
}
