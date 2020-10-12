import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroChangeLocationComponent } from './hero-change-location.component';

describe('HeroChangeLocationComponent', () => {
  let component: HeroChangeLocationComponent;
  let fixture: ComponentFixture<HeroChangeLocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroChangeLocationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroChangeLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
