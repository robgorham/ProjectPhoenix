import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { IBoard, IColumn } from './board-models';


@Injectable({
  providedIn: 'root'
})
export class BoardApiService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  addItemCardToColumnById(boardId: any, columnId: any, name: string): Observable<any> {
    return this.http.post(this.baseUrl + 'api/itemcards', { boardId, columnId, name }).pipe(
      tap(res => console.log(JSON.stringify(res)))
    );
  }

  moveItemCardInArray(postData: any): Observable<any> {
    return this.http.post(this.baseUrl + 'api/itemcards/movebulk', { ...postData }).pipe(tap(console.log))
  }



  deleteItemCardById(cardId: any) {
    return this.http.delete(this.baseUrl + 'api/itemcards/' + cardId).pipe(
      tap(console.log)
    );
  }

  addColumn(board: IBoard, name: string): Observable<any> {
    return this.http.post(this.baseUrl + 'api/boards/' + board.id + '/columns', { name }).pipe(
      tap(res => console.log(JSON.stringify(res)))
    );
  }

  updateColumnById(id: string, column: IColumn) {
    return this.http.put<number>(this.baseUrl + 'api/columns/' + id, column).pipe(
      tap(console.log)
    );
  }

  deleteColumnById(id: string) {
    return this.http.delete<number>(this.baseUrl + 'api/columns/' + id).pipe(
      tap(console.log)
    );
  }

  getBoards(): Observable<IBoard[]> {
    return this.http.get<IBoard[]>(this.baseUrl + 'api/boards').pipe(
      tap(console.log)
    );
  }

  getBoardById(id: string): Observable<IBoard> {
    return this.http.get<IBoard>(this.baseUrl + 'api/boards/' + id).pipe(
    )
  }

  updateBoardById(id: string, board: IBoard) {
    return this.http.put<number>(this.baseUrl + 'api/boards/' + id, { name: board.name, board }).pipe(
      tap(console.log)
    );
  }

  deleteBoardById(id: string) {
    return this.http.delete<number>(this.baseUrl + 'api/boards/' + id).pipe(
      tap(console.log)
    );
  }

  createBoard(name: string) {
    return this.http.post(this.baseUrl + 'api/boards', { name }).pipe(
      tap(console.log)
    );
  }
}
