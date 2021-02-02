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
  loading = false;
  submitting = false;
  error: AxiosResponse | null = null;

  get fields(): IField[] {
    return Array.from(this.fieldRegistry.values());
  }

  loadFields = async () => {
    this.loading = true;
    try {
      const { fields } = await agent.Fields.list();
      runInAction(() => {
        fields.forEach((field) => {
          this.fieldRegistry.set(field.id, field);
        });
        this.loading = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loading = false;
      });
      console.log(error);
    }
  };

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
