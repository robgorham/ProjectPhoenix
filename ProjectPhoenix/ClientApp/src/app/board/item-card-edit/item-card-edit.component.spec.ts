import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemCardEditComponent } from './item-card-edit.component';

describe('ItemCardEditComponent', () => {
  let component: ItemCardEditComponent;
  let fixture: ComponentFixture<ItemCardEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ItemCardEditComponent ]
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
