import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import { toast } from 'react-toastify';
import agent from '../api/agent';
import { IField } from '../models/field';
import { RootStore } from './rootStore';

export default class FieldStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }

  fieldRegistry = new Map();
  submitting = false;
  error: AxiosResponse | null = null;

  createField = async (field: IField) => {
    this.submitting = true;
    try {
      field.id = await agent.Fields.create(field);
      runInAction(() => {
        this.fieldRegistry.set(field.id, field);
        this.submitting = false;
      });
      toast.success('Field created successfully');
    } catch (error) {
      runInAction(() => {
        this.submitting = false;
        this.error = error;
      });
      console.log(error);
    }
  };
}
