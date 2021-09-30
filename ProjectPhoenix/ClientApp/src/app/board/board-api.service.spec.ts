import { inject, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { BoardApiService } from './board-api.service';

describe('BoardApiService', () => {

  beforeEach(() => TestBed.configureTestingModule({
    providers: [BoardApiService,
      {provide: 'BASE_URL', useValue: 'http://localhost'}
    ],
    imports: [HttpClientTestingModule]
  }));

  it('should be created', inject([BoardApiService], (service: BoardApiService)=> {
    expect(service).toBeTruthy();
  }));
});
