import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { filter, switchMap, tap } from 'rxjs/operators';
import { BoardApiService } from '../board-api.service';
import { BoardEditComponent } from '../board-edit/board-edit.component';
import { IBoard } from '../board-models';


export interface BoardEditDialogData {
  name: string;
  id: string;
}


@Component({
  selector: 'app-board-manager',
  templateUrl: './board-manager.component.html',
  styleUrls: ['./board-manager.component.css']
})
export class BoardManagerComponent implements OnInit {

  boards$: Observable<IBoard[]>;
  constructor(private boardapi: BoardApiService, public dialog: MatDialog) { }


  ngOnInit() {
    this.boards$ = this.boardapi.getBoards();
  }

  openEditDialog(name: string, id: string) {
    const dialogRef = this.dialog.open(BoardEditComponent,
      {
        width: '250px',
        data: { name, id },
        disableClose: true
      })

    dialogRef.afterClosed().pipe(
      filter(result => result.success),
      tap(console.log),
      switchMap(result => this.boardapi.updateBoardById(id, result.name)),
      tap(() => this.boards$ = this.boardapi.getBoards())
    ).subscribe();
  }

  deleteBoard(id: string) {
    this.boardapi.deleteBoardById(id).pipe(
      switchMap(() => this.boards$ = this.boardapi.getBoards())
    ).subscribe();
  }
}
