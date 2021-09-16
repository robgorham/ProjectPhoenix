import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { debounceTime, filter, finalize, startWith, switchMap, take, tap, withLatestFrom } from 'rxjs/operators';
import { BoardApiService } from '../board-api.service';
import { BoardEditComponent } from '../board-edit/board-edit.component';
import { IBoard, IColumn, mockBoards } from '../board-models';
import { ConfirmModalComponent } from '../confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BoardComponent implements OnInit {

  board$: BehaviorSubject<IBoard> = new BehaviorSubject(null);

  constructor(private route: ActivatedRoute, public dialog: MatDialog,
    private router: Router, private boardapi: BoardApiService) {

  }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap(params => this.boardapi.getBoardById(params.get('id'))),
      tap(board => this.board$.next(board))
    ).subscribe();
  }

  oc(obj: any): void {
    console.log(`obj: ${JSON.stringify(obj)}`);
  }
  openAddItemCardDialog(column: IColumn): void {

  }
  onItemCardMoveEvent(event: CdkDragDrop<any[]>, board: IBoard): void {
    console.log(event.container.id);
    const currContainer = event.container;
    const prevContainer = event.previousContainer;
    const currIndex = event.currentIndex;
    const prevIndex = event.previousIndex;
    if (currContainer.id === prevContainer.id) {
      // kinda trivial
      let colId: any = false;
      let i = -1;
      // must find the column in question
      board.columns.some((col, idx) => {
        if (col.id === currContainer.id) {
          colId = col.id;
          i = idx;
          return true;
        }
        return false;
      });
      console.log(colId, i);
      // must move the item in the array
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
      // must recreate the order
      const cardResults = [...event.container.data.map((card, idx) => ({ ...card, order: idx }))];
      event.container.data = [...cardResults];
      // must map it back into the board, if I need too... :-D
      board.columns[i].itemCards = cardResults;
      console.log('same');
      this.boardapi.updateColumnById(board.columns[i].id, board.columns[i]).subscribe( res => console.log);
    }
    else {
      // must find both arrays
      // must remove item from first
      // must add into second at appropriate index
      // must recreate the order
      console.log('different')
    }
    // must broadcast the result
    // must update the board
    this.board$.next({ ...board });
    //this.boardapi.updateBoardById(board.id, {...board }).subscribe();
  }

  getColumnIDs(board: IBoard): any[] {
    const result = board.columns.map(col => `${col.id}`);
    return result;
  }
  onColumnMove(event: CdkDragDrop<IColumn[]>, board: IBoard) {
    //console.log(JSON.stringify(event.container))
    console.log('oncolumnmove');
    const cols = board.columns;
    moveItemInArray(board.columns, event.previousIndex, event.currentIndex);
    const boardResult = { ...board, columns: [...cols.map((column, idx) => ({ ...column, order: idx }))] };
    this.board$.next(boardResult);
    this.boardapi.updateBoardById(boardResult.id, boardResult).subscribe();
  }
  openColumnEditDialog(name: string, id: string): void {
    const dialogRef = this.dialog.open(BoardEditComponent,
      {
        data: { name, id },
        disableClose: true
      });
    dialogRef.afterClosed().pipe(
      filter(result => result.success),
      tap(console.log),
      switchMap(result => this.boardapi.updateColumnById(id, result.name)),
      withLatestFrom(this.board$),
      switchMap(([_, board]) => this.boardapi.getBoardById(board.id)),
      tap(board => this.board$.next(board))
    ).subscribe();

  }



  deleteColumn(id: string): void {
    const dialogRef = this.dialog.open(ConfirmModalComponent, {
      disableClose: true
    })

    dialogRef.afterClosed().pipe(
      filter(result => result.success),
      switchMap(() => this.boardapi.deleteColumnById(id)),
      withLatestFrom(this.board$),
      switchMap(([_, board]) => this.boardapi.getBoardById(board.id)),
      tap(board => this.board$.next(board))
    ).subscribe();
  }
  onAddColumnClick(board: IBoard) {
    this.boardapi.addColumn(board, 'Blank ').pipe(
      debounceTime(500),
      switchMap(_ => this.boardapi.getBoardById(board.id)),
      tap(updatedBoard => this.board$.next(updatedBoard))
    ).subscribe();

  }

  onAddItemCardSubmit(result: any): void {
    this.boardapi.addItemCardToColumnById(result.boardId, result.columnId, result.name).pipe(
      debounceTime(500),
      switchMap(_ => this.boardapi.getBoardById(result.boardId)),
      tap(updatedBoard => this.board$.next(updatedBoard))
    ).subscribe();
  }

}
