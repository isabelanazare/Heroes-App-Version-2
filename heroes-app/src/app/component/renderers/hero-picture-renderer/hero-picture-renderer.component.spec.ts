import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroPictureRendererComponent } from './hero-picture-renderer.component';

describe('HeroPictureRendererComponent', () => {
  let component: HeroPictureRendererComponent;
  let fixture: ComponentFixture<HeroPictureRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroPictureRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroPictureRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
