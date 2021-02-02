import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { ICity } from '../models/city';
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

  get cities(): ICity[] {
    return Array.from(this.cityRegistry.values());
  }

  loadCities = async () => {
    this.loading = true;
    try {
      const { cities } = await agent.Cities.list();
      runInAction(() => {
        cities.forEach((city) => {
          this.cityRegistry.set(city.id, city);
        });
        this.loading = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loading = false;
        this.error = error;
      });
      console.log(error);
    }
  };
}
