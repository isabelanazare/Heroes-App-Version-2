import { Component, OnInit } from '@angular/core';
import { ChartsService } from '../../../service/charts.service';
import { BarChart } from '../../../models/bar-chart';

@Component({
  selector: 'app-right-chart',
  templateUrl: './right-chart.component.html',
  styleUrls: ['./right-chart.component.scss'],
})
export class RightChartComponent extends BarChart implements OnInit {
  constructor(private chartsService: ChartsService) {
    super();
  }

  ngOnInit() {
    this.initializeChartType('doughnut');
    this.startLoading();
    this._getPowerChart();
  }

  private _getPowerChart() {
    this.chartsService.getPowerBarChart().subscribe((chartData) => {
      let heroNumbers: any = [];
      chartData.data.forEach((element) => {
        heroNumbers.push(element.data[0]);
      });
      this.barChartData.push({ data: heroNumbers });
      this.barChartLabels = chartData.chartXData;
      this.stopLoading();
    });
  }
}
