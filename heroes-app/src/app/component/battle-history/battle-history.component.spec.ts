import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BattleHistoryComponent } from './battle-history.component';

describe('BattleHistoryComponent', () => {
  let component: BattleHistoryComponent;
  let fixture: ComponentFixture<BattleHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BattleHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BattleHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
