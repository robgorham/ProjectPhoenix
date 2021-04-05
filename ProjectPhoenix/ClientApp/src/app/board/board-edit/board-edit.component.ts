import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BoardEditDialogData } from '../board-manager/board-manager.component';

@Component({
  selector: 'app-board-edit',
  templateUrl: './board-edit.component.html',
  styleUrls: ['./board-edit.component.css']
})
export class BoardEditComponent implements OnInit {


  constructor(public dialogRef: MatDialogRef<BoardEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BoardEditDialogData) { }

  ngOnInit(): void {
  }

  onCancelClick() {
    this.dialogRef.close({success: false});
  }

}
