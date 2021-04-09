import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { switchMap, take, tap } from 'rxjs/operators';
import { BoardApiService } from '../board-api.service';
import { IBoard, IColumn, mockBoards } from '../board-models';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BoardComponent implements OnInit {

  board$: Observable<IBoard>;
  board: IBoard;
  columns: IColumn[];

  constructor(private route: ActivatedRoute,
    private router: Router, private boardapi: BoardApiService) {
    
  }

  ngOnInit() {
    this.board$ = this.route.paramMap.pipe(
      take(1),
      switchMap((params: ParamMap) => this.boardapi.getBoardById(params.get('id')).pipe(take(1), tap(console.log))
      ));


    //#region mock stuff
    let mb = { ...mockBoards[0] };
    const cols: IColumn[] = Array.from({ length: 8 }).map((_, i) => ({ ...mb.columns[0], id: i.toString(), name: `col ${i + 1}`, order:i }));
    mb.columns = [...cols];
    this.board = mb;
    //#endregion

    //this.columns = cols;
  }

  onColumnMove(event: CdkDragDrop<IColumn[]>, board: IBoard) {
    const cols = board.columns;
    console.log('board.column before moved', JSON.stringify(board.columns.map(x => x.id)));
    //moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    moveItemInArray(board.columns, event.previousIndex, event.currentIndex);
    console.log('column maybe moved', JSON.stringify(board.columns.map(x => ({ id: x.id, name: x.name }))));
    this.board = { ...board, columns: [...cols] };
  }
  onAddColumnClick(board: IBoard) {
    this.boardapi.addColumn(board, 'Blank').subscribe();

  }

}
