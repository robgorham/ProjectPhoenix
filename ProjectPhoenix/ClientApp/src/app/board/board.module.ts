import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BoardRoutingModule } from './board-routing.module';
import { BoardComponent } from '../board/board/board.component';
import { BoardManagerComponent } from '../board/board-manager/board-manager.component';


@NgModule({
  declarations: [BoardComponent, BoardManagerComponent],
  imports: [
    CommonModule,
    BoardRoutingModule
  ],
  exports: [BoardComponent, BoardManagerComponent]
})
export class BoardModule { }
