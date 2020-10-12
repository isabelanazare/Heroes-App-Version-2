import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditHeroModalComponent } from './edit-hero-modal.component';

describe('EditHeroModalComponent', () => {
  let component: EditHeroModalComponent;
  let fixture: ComponentFixture<EditHeroModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditHeroModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditHeroModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
