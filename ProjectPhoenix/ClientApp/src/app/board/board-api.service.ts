import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { IBoard } from './board-models';


@Injectable({
  providedIn: 'root'
})
export class BoardApiService {

  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string) {

  }

  addColumn(board: IBoard, name: string) : Observable<any> {
    return this.http.post(this.baseUrl + 'api/boards/' + board.id + '/columns', { name }).pipe(
      tap(res => console.log(JSON.stringify(res)))
    );
  }

  updateColumnById(id: string, name: string){
    return this.http.put<number>(this.baseUrl + 'api/columns/' + id, { name }).pipe(
      tap(console.log)
    );
  }

  deleteColumnById(id: string) {
    return this.http.delete<number>(this.baseUrl + 'api/columns/' + id).pipe(
      tap(console.log)
    );
  }

  getBoards(): Observable<IBoard[]>{
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
