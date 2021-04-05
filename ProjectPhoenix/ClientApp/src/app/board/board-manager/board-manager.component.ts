import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BoardApiService } from '../board-api.service';

@Component({
  selector: 'app-board-manager',
  templateUrl: './board-manager.component.html',
  styleUrls: ['./board-manager.component.css']
})
export class BoardManagerComponent implements OnInit {

  boards$: Observable<IBoard[]>;
  constructor(private boardapi: BoardApiService) { }

  ngOnInit() {
    this.boards$ = this.boardapi.getBoards();
  }

}
