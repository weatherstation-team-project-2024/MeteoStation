import { TestBed } from '@angular/core/testing';

import { DatastoreServiceTsService } from './datastore.service.ts.service';

describe('DatastoreServiceTsService', () => {
  let service: DatastoreServiceTsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DatastoreServiceTsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
