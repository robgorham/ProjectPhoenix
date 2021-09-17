import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { filter, switchMap, tap } from 'rxjs/operators';
import { BoardApiService } from '../board-api.service';
import { BoardEditComponent } from '../board-edit/board-edit.component';
import { IColumn } from '../board-models';
import { ItemCardEditComponent } from '../item-card-edit/item-card-edit.component';

@Component({
  selector: 'app-column',
  templateUrl: './column.component.html',
  styleUrls: ['./column.component.css']
})
export class ColumnComponent implements OnInit {

  constructor(public dialog: MatDialog, private boardapi: BoardApiService) { }

  @Input() column: any;
  @Input() linked: any[];
  // tslint:disable-next-line: no-output-on-prefix
  @Output() onEditClick = new EventEmitter<{ name: string, id: string }>();
  @Output() onDeleteClick = new EventEmitter<string>();
  @Output() onCardDeleteClick = new EventEmitter<string>();
  @Output() onEditItemCard = new EventEmitter<{name: string, id: string, description: string}>();
  @Output() addItemCardClick = new EventEmitter();
  @Output() onItemCardMove = new EventEmitter<CdkDragDrop<any[]>>();
  ngOnInit(): void {
  }
  openEditDialog(name: string, id: string): void {
    this.onEditClick.emit({ name: name, id: id });
  }
  openDeleteDialog(id: string): void {
    this.onDeleteClick.emit(id);
  }
  openCardDeleteDialog(id: string): void {
    this.onCardDeleteClick.emit(id);
  }
  addItemCard(template): void {
    // add dialog ref
    const dialogRef = this.dialog.open(template, {
      disableClose: true
    });
    // catch after its closed
    dialogRef.afterClosed().pipe(
      filter(res => res.success),
      tap(res => {
        console.log(res);
        this.addItemCardClick.emit({columnId: this.column.id, name: res.name, boardId: this.column.boardId })
      })
    ).subscribe();

  }

  openEditCardDialog(card) {
    console.log("hit")
    const dialogRef = this.dialog.open(ItemCardEditComponent, {
      data: {name: card.name, id: card.id, description: card.description },
      disableClose: true
    });

    dialogRef.afterClosed().pipe(
      filter(result => result.success),
      tap(res => {
        console.log(res);
        this.onEditItemCard.emit({ name: res.name, description: res.description, id: res.id });
      })
    ).subscribe();
  }
  drop(event: CdkDragDrop<any[]>) {
    //console.log(JSON.stringify(event))
    this.onItemCardMove.emit(event);
  }

}
