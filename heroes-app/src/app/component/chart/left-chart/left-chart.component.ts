import { Component, OnInit } from '@angular/core';
import { ChartsService } from '../../../service/charts.service';
import { BarChart } from '../../../models/bar-chart';

@Component({
  selector: 'app-left-chart',
  templateUrl: './left-chart.component.html',
  styleUrls: ['./left-chart.component.scss'],
})
export class LeftChartComponent extends BarChart implements OnInit {
  constructor(private chartservice: ChartsService) {
    super();
  }

  ngOnInit() {
    this.initializeChartType('bar');
    this.startLoading();
    this._getHeroesBarChart();
  }

  private _getHeroesBarChart() {
    this.chartservice.getHeroesPowerBarChart().subscribe((chartData) => {
      this.barChartData = chartData.data;
      this.barChartLabels = chartData.chartXData;
      this.stopLoading();
    });
  }
}
