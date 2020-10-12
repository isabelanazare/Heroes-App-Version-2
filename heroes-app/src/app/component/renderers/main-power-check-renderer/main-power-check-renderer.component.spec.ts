import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPowerCheckRendererComponent } from './main-power-check-renderer.component';

describe('MainPowerCheckRendererComponent', () => {
  let component: MainPowerCheckRendererComponent;
  let fixture: ComponentFixture<MainPowerCheckRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainPowerCheckRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainPowerCheckRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
