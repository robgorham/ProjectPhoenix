import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of, Subject } from "rxjs";
import { BoardApiService } from "./board-api.service";

export interface IBaseModel {
  id: string;
  createDate: Date;
  modifyDate: Date;
}

export interface IBoard extends IBaseModel {
  name: string;
  username: string;
  columns?: IColumn[] | null;
}

export interface IColumn extends IBaseModel {
  name: string;
  order: number;  // This needs to be unique among the columns associated to a Board
  username: string;
}

const now = new Date();

export const mockColumns: IColumn[] =
  [{
    name: "col1",
    username: "rgorham",
    order: 0,
    id: "1",
    createDate: now,
    modifyDate: now

  }, {
      name: "col2",
      order: 1,
      username: "rgorham",
      id: "2",
      createDate: now,
      modifyDate: now

    }];

export const mockBoards: IBoard[] =
  [{
    name: "board1",
    username: "rgorham",
    id: "1",
    createDate: now,
    modifyDate: now,
    columns: mockColumns
  }]
