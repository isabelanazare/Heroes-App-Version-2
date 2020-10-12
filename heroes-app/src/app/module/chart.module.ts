import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeftChartComponent } from '../component/chart/left-chart/left-chart.component';
import { RightChartComponent } from '../component/chart/right-chart/right-chart.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    LeftChartComponent,
    RightChartComponent
  ],
  imports: [
    ChartsModule,
    CommonModule,
    FlexLayoutModule
  ],
  exports: [
    LeftChartComponent,
    RightChartComponent
  ]

})
export class ChartModule { }
