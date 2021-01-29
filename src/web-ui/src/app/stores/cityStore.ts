import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { RootStore } from './rootStore';

export default class CityStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }

  cityRegistry = new Map();
  loading = false;
  error: AxiosResponse | null = null;

  get cities() {
    return Array.from(this.cityRegistry.values());
  }

  loadCities = async () => {
    this.loading = true;
    try {
      const citiesVm = await agent.Cities.list();
      const { cities } = citiesVm;
    } catch (error) {
      runInAction(() => {
        this.loading = true;
        this.error = error;
      });
      console.log(error);
    }
  };
}
