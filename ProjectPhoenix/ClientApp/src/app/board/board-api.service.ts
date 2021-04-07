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

  getBoards(): Observable<IBoard[]>{
    return this.http.get<IBoard[]>(this.baseUrl + 'api/boards').pipe(
      tap(console.log)
    );
  }

  updateBoardById(id: string, name: string) {
    return this.http.put<number>(this.baseUrl + 'api/boards/' + id, { name }).pipe(
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
