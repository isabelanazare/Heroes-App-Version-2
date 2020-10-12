import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditHeropowerModalComponent } from './edit-heropower-modal.component';

describe('EditHeropowerModalComponent', () => {
  let component: EditHeropowerModalComponent;
  let fixture: ComponentFixture<EditHeropowerModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditHeropowerModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditHeropowerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
