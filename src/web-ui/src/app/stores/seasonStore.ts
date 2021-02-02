import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import { toast } from 'react-toastify';
import agent from '../api/agent';
import { IPlayerStats, ISeason, IStandings } from '../models/season';
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
  playersStatsRegistry = new Map();
  loading = false;
  loadingStandings = false;
  loadingPlayersStats = false;
  submitting = false;
  error: AxiosResponse | null = null;
  predicate = new Map();

  get seasons() {
    return Array.from(this.seasonRegistry.values());
  }

  get playersStats(): IPlayerStats[] {
    return Array.from(this.playersStatsRegistry.values());
  }

  loadSeasons = async () => {
    this.loading = true;
    try {
      const { seasons } = await agent.Seasons.list();
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

  createSeason = async (season: ISeason) => {
    this.submitting = true;
    try {
      season.id = await agent.Seasons.create(season);
      //get city and store it in league.city
      runInAction(() => {
        this.seasonRegistry.set(season.id, season);
        this.submitting = false;
      });
      toast.success('Season created successfully');
    } catch (error) {
      runInAction(() => {
        this.submitting = false;
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

  loadPlayersStats = async (id: number) => {
    this.loadingPlayersStats = true;
    try {
      const playersStats = await agent.Seasons.playersStats(id);
      const { players } = playersStats;
      runInAction(() => {
        this.playersStatsRegistry.clear();
        players.forEach((player) => {
          this.playersStatsRegistry.set(player.id, player);
        });
        this.loadingPlayersStats = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingPlayersStats = false;
      });
      console.log(error);
    }
  };
}
