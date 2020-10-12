import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnauthorisedPageComponent } from './unauthorised-page.component';

describe('UnauthorisedPageComponent', () => {
  let component: UnauthorisedPageComponent;
  let fixture: ComponentFixture<UnauthorisedPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnauthorisedPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnauthorisedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
