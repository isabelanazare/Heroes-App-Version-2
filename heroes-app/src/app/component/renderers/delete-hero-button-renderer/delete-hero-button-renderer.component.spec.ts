import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteHeroButtonRendererComponent } from './delete-hero-button-renderer.component';

describe('DeleteHeroButtonRendererComponent', () => {
  let component: DeleteHeroButtonRendererComponent;
  let fixture: ComponentFixture<DeleteHeroButtonRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteHeroButtonRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteHeroButtonRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
