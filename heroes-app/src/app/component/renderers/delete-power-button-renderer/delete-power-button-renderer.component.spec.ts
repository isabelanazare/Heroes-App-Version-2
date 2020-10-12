import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePowerButtonRendererComponent } from './delete-power-button-renderer.component';

describe('DeletePowerButtonRendererComponent', () => {
  let component: DeletePowerButtonRendererComponent;
  let fixture: ComponentFixture<DeletePowerButtonRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeletePowerButtonRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeletePowerButtonRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
