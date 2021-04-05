import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSliderModule } from '@angular/material/slider';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { BoardRoutingModule } from './board-routing.module';
import { MatRippleModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';


import { BoardComponent } from '../board/board/board.component';
import { BoardManagerComponent } from '../board/board-manager/board-manager.component';


@NgModule({
  declarations: [BoardComponent, BoardManagerComponent],
  imports: [
    CommonModule,
    BoardRoutingModule,
    MatSliderModule,
    MatCardModule,
    MatIconModule,
    MatRippleModule,
    MatButtonModule
  ],
  exports: [BoardComponent, BoardManagerComponent]
})
export class BoardModule { }
