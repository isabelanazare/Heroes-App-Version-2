import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VillainPowerTableComponent } from './villain-power-table.component';

describe('VillainPowerTableComponent', () => {
  let component: VillainPowerTableComponent;
  let fixture: ComponentFixture<VillainPowerTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VillainPowerTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VillainPowerTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
