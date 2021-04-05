interface IBaseModel {
  id: string;
  createDate: Date;
  modifyDate: Date;
}
interface IBoard extends IBaseModel {
  name: string;
}
