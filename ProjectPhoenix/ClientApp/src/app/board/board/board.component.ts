import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { BoardApiService } from '../board-api.service';
import { IBoard, MockBoardApiService } from '../board-models';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

  board$: Observable<IBoard>;
  m
  constructor(private route: ActivatedRoute,
    private router: Router,
    private boardapi: MockBoardApiService) {
  }

  ngOnInit() {
    this.board$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => this.boardapi.getBoardById(params.get('id'))
      ));
  }

}
