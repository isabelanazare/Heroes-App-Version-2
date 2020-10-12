import { TestBed } from '@angular/core/testing';

import { HeroTypeService } from './hero-type.service';

describe('HeroTypeService', () => {
  let service: HeroTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HeroTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
