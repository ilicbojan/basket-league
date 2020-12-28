import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { ISeason, IStandings } from '../models/season';
import { RootStore } from './rootStore';
export default class SeasonStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }

  seasonRegistry = new Map();
  season: ISeason | null = null;
  standingsRegistry = new Map();
  standings: IStandings | null = null;
  loading = false;
  loadingStandings = false;
  submitting = false;
  error: AxiosResponse | null = null;
  predicate = new Map();

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

  loadSeason = async (id: number) => {
    let season: ISeason = this.getSeason(id);
    if (season) {
      this.season = season;
    } else {
      this.loading = true;
      try {
        season = await agent.Seasons.details(id);
        runInAction(() => {
          this.seasonRegistry.set(season.id, season);
          this.season = season;
          this.loading = false;
        });
      } catch (error) {
        runInAction(() => {
          this.loading = false;
        });
        console.log(error);
      }
    }
  };

  getSeason = (id: number): ISeason => {
    return this.seasonRegistry.get(id);
  };

  loadStandings = async (id: number) => {
    let standings: IStandings = this.getStandings(id);
    if (standings) {
      this.standings = standings;
    } else {
      this.loadingStandings = true;
      try {
        standings = await agent.Seasons.standings(id);
        runInAction(() => {
          this.standingsRegistry.set(standings.seasonId, standings);
          this.standings = standings;
          this.loadingStandings = false;
        });
      } catch (error) {
        runInAction(() => {
          this.loadingStandings = false;
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
