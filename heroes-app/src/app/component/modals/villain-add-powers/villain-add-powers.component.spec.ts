import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VillainAddPowersComponent } from './villain-add-powers.component';

describe('VillainAddPowersComponent', () => {
  let component: VillainAddPowersComponent;
  let fixture: ComponentFixture<VillainAddPowersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VillainAddPowersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VillainAddPowersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
