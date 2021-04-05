export interface IBaseModel {
  id: string;
  createDate: Date;
  modifyDate: Date;
}

export interface IBoard extends IBaseModel {
  name: string;
  username: string;
}

