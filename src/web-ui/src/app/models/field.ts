export interface IFieldsVm {
  fields: IField[];
}

export interface IField {
  id: number;
  name: string;
  address: string;
  cityId: number;
}
