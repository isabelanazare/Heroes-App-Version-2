import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatePickerRendererComponent } from './date-picker-renderer.component';

describe('DatePickerRendererComponent', () => {
  let component: DatePickerRendererComponent;
  let fixture: ComponentFixture<DatePickerRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatePickerRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatePickerRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
