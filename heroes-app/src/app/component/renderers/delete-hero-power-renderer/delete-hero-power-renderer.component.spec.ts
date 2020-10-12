import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteHeroPowerRendererComponent } from './delete-hero-power-renderer.component';

describe('DeleteHeroPowerRendererComponent', () => {
  let component: DeleteHeroPowerRendererComponent;
  let fixture: ComponentFixture<DeleteHeroPowerRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteHeroPowerRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteHeroPowerRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
