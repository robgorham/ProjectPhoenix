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

  constructor(private route: ActivatedRoute,public dialog: MatDialog,
    private router: Router, private boardapi: BoardApiService) {
    
  }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap(params => this.boardapi.getBoardById(params.get('id'))),
      tap(board => this.board$.next(board))
    ).subscribe();
  }

  oc(obj: any): void{
    console.log(`obj: ${JSON.stringify(obj)}`);
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
        switchMap(([_, board])=> this.boardapi.getBoardById(board.id)),
        tap(board => this.board$.next(board))
      ).subscribe();

  }

  onColumnMove(event: CdkDragDrop<IColumn[]>, board: IBoard) {
    const cols = board.columns;
    console.log('board.column before moved', JSON.stringify(board.columns.map(x => x.id)));
    // moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    moveItemInArray(board.columns, event.previousIndex, event.currentIndex);
    console.log('column maybe moved', JSON.stringify(board.columns.map(x => ({ id: x.id, name: x.name, order: x.order }))));
    const boardResult = { ...board, columns: [...cols.map((column, idx) => ({...column, order: idx}))] };
    this.board$.next(boardResult);
    this.boardapi.updateBoardById(boardResult.id, boardResult).subscribe();
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

}
