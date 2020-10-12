import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroBadgesComponent } from './hero-badges.component';

describe('HeroBadgesComponent', () => {
  let component: HeroBadgesComponent;
  let fixture: ComponentFixture<HeroBadgesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroBadgesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroBadgesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
