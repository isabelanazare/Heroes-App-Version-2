import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPowersTableComponent } from './user-powers-table.component';

describe('UserPowersTableComponent', () => {
  let component: UserPowersTableComponent;
  let fixture: ComponentFixture<UserPowersTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserPowersTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPowersTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
