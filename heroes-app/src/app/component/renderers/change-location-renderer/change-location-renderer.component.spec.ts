import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeLocationRendererComponent } from "./ChangeLocationRendererComponent";

describe('ChangeLocationRendererComponent', () => {
  let component: ChangeLocationRendererComponent;
  let fixture: ComponentFixture<ChangeLocationRendererComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeLocationRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeLocationRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
