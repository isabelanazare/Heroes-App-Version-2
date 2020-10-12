import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VillainImageRendererComponent } from './villain-image-renderer.component';

describe('VillainImageRendererComponent', () => {
  let component: VillainImageRendererComponent;
  let fixture: ComponentFixture<VillainImageRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VillainImageRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VillainImageRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
