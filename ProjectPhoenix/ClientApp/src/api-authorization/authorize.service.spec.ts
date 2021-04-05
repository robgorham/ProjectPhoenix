import { TestBed, inject } from '@angular/core/testing';

import { AuthorizeService } from './authorize.service';



fdescribe('AuthorizeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthorizeService]
    });
  });

  it('should be created', inject([AuthorizeService], (service: AuthorizeService) => {
    expect(service).toBeTruthy();
  }));
});
