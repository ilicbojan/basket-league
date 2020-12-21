import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { IStandings } from '../models/season';
import { RootStore } from './rootStore';

export default class SeasonStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }

  seasonRegistry = new Map();
  standingsRegistry = new Map();
  standings: IStandings | null = null;
  loading = false;
  submitting = false;
  error: AxiosResponse | null = null;

  get seasons() {
    return Array.from(this.seasonRegistry.values());
  }

  loadSeasons = async () => {
    this.loading = true;
    try {
      const seasons = await agent.Seasons.list();
      runInAction(() => {
        seasons.forEach((season) => {
          this.seasonRegistry.set(season.id, season);
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

  loadStandings = async (id: number) => {
    let standings: IStandings = this.getStandings(id);
    if (standings) {
      this.standings = standings;
    } else {
      this.loading = true;
      try {
        standings = await agent.Seasons.standings(id);
        runInAction(() => {
          this.standingsRegistry.set(standings.id, standings);
          this.standings = standings;
          this.loading = false;
        });
      } catch (error) {
        runInAction(() => {
          this.loading = false;
          this.error = error;
        });
        console.log(error);
      }
    }
  };

  getStandings = (id: number): IStandings => {
    return this.standingsRegistry.get(id);
  };
}
