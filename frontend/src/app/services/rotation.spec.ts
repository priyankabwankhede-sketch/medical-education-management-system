import { TestBed } from '@angular/core/testing';

import { Rotation } from './rotation';

describe('Rotation', () => {
  let service: Rotation;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Rotation);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
