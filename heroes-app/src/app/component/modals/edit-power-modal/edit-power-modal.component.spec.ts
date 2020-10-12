import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPowerModalComponent } from './edit-power-modal.component';

describe('EditPowerModalComponent', () => {
  let component: EditPowerModalComponent;
  let fixture: ComponentFixture<EditPowerModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPowerModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPowerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
