import { TestBed, inject } from '@angular/core/testing';
import { RouterModule } from '@angular/router';

import { AuthorizeGuard } from './authorize.guard';

describe('AuthorizeGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterModule.forRoot([])],
      providers: [AuthorizeGuard]
    });
  });

  it('should ...', inject([AuthorizeGuard], (guard: AuthorizeGuard) => {
    expect(guard).toBeTruthy();
  }));
});
