import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';


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
}
