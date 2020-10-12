import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVillainModalComponent } from './add-villain-modal.component';

describe('AddVillainModalComponent', () => {
  let component: AddVillainModalComponent;
  let fixture: ComponentFixture<AddVillainModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddVillainModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddVillainModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
