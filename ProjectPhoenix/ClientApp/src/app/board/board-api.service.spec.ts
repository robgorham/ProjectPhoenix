import { TestBed } from '@angular/core/testing';

import { BoardApiService } from './board-api.service';

describe('BoardApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BoardApiService = TestBed.get(BoardApiService);
    expect(service).toBeTruthy();
  });
});
