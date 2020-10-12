import { TestBed } from '@angular/core/testing';

import { HeroPowerService } from './hero-power.service';

describe('HeroPowerService', () => {
  let service: HeroPowerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HeroPowerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
