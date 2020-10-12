import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts/ng2-charts';
import { LoadingData } from '../utils/loading-data';

export abstract class BarChart extends LoadingData {
  public barChartOptions: ChartOptions = {
    responsive: true,
    legend: {
      display: false,
    },
  };
  public barChartLabels: Label[] = [];
  public barChartType: ChartType;
  public barChartData: ChartDataSets[] = [];

  public initializeChartType(type: ChartType) {
    this.barChartType = type;
  }
}
