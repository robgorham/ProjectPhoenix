import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { BoardEditComponent } from './board-edit.component';

describe('BoardEditComponent', () => {
  let component: BoardEditComponent;
  let fixture: ComponentFixture<BoardEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BoardEditComponent],
      imports: [MatDialogModule, MatFormFieldModule,
        MatInputModule, NoopAnimationsModule],
      providers: [
        {
          provide: MAT_DIALOG_DATA, useValue: {
            data: {name: 'Rob is cool', id: 1}
          }
        },
        { provide: MatDialogRef, useValue: {} },
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoardEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
