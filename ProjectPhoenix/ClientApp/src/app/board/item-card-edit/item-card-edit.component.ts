import { Component, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface IItemCardEditData {
  name: string;
  description: string;
  id: string;
}
@Component({
  selector: 'app-item-card-edit',
  templateUrl: './item-card-edit.component.html',
  styleUrls: ['./item-card-edit.component.css']
})
export class ItemCardEditComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ItemCardEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IItemCardEditData) { }
  ngOnInit(): void {

  }

}
