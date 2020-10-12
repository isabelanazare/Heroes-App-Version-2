import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHeroModalComponent } from './add-hero-modal.component';

describe('AddHeroModalComponent', () => {
  let component: AddHeroModalComponent;
  let fixture: ComponentFixture<AddHeroModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddHeroModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddHeroModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
