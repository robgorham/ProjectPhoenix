import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSliderModule } from '@angular/material/slider';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { BoardRoutingModule } from './board-routing.module';
import { MatRippleModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';


import { BoardComponent } from '../board/board/board.component';
import { BoardManagerComponent } from '../board/board-manager/board-manager.component';
import { BoardEditComponent } from './board-edit/board-edit.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [BoardComponent, BoardManagerComponent, BoardEditComponent],
  imports: [
    CommonModule,
    BoardRoutingModule,
    MatSliderModule,
    FormsModule,
    MatCardModule,
    MatIconModule,
    MatRippleModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule
  ],
  exports: [BoardComponent, BoardManagerComponent]
})
export class BoardModule { }
