import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RightChartComponent } from './right-chart.component';

describe('RightChartComponent', () => {
  let component: RightChartComponent;
  let fixture: ComponentFixture<RightChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RightChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RightChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
