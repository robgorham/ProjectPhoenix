import { Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Component,Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { filter, switchMap, tap } from 'rxjs/operators';
import { BoardApiService } from '../board-api.service';
import { BoardEditComponent } from '../board-edit/board-edit.component';
import { IColumn } from '../board-models';

@Component({
  selector: 'app-column',
  templateUrl: './column.component.html',
  styleUrls: ['./column.component.css']
})
export class ColumnComponent implements OnInit {

  constructor(public dialog: MatDialog, private boardapi: BoardApiService) { }

  @Input() column: any;
  // tslint:disable-next-line: no-output-on-prefix
  @Output() onEditClick = new EventEmitter<{ name: string, id: string }>();
  @Output() onDeleteClick = new EventEmitter<{ id: string }>();
  @Output() addItemCardClick = new EventEmitter<{ id: string }>();
  ngOnInit(): void {
  }
  openEditDialog(name: string, id: string): void {
    this.onEditClick.emit({name: name, id: id});
  }
  openDeleteDialog(id: string): void {
    this.onDeleteClick.emit({ id });
  }
  addItemCard(id: string): void {
    this.addItemCardClick.emit({ id });
  }

}
