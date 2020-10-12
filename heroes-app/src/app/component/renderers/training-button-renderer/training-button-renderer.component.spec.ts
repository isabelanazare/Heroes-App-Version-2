import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingButtonRendererComponent } from './training-button-renderer.component';

describe('TrainingButtonRendererComponent', () => {
  let component: TrainingButtonRendererComponent;
  let fixture: ComponentFixture<TrainingButtonRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrainingButtonRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingButtonRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
