import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPowerModalComponent } from './add-power-modal.component';

describe('AddPowerModalComponent', () => {
  let component: AddPowerModalComponent;
  let fixture: ComponentFixture<AddPowerModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddPowerModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPowerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
