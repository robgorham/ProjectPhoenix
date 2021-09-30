import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { ItemCardEditComponent } from './item-card-edit.component';

describe('ItemCardEditComponent', () => {
  let component: ItemCardEditComponent;
  let fixture: ComponentFixture<ItemCardEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ItemCardEditComponent],
      imports: [MatDialogModule, MatFormFieldModule,
        MatInputModule, NoopAnimationsModule],
      providers: [{
        provide: MAT_DIALOG_DATA, useValue: {
          data: { name: 'Rob is cool', id: 1 }
        }
      },
      { provide: MatDialogRef, useValue: {} },]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemCardEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
