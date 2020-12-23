import { AxiosResponse } from 'axios';
import { makeAutoObservable, reaction, runInAction } from 'mobx';
import agent from '../api/agent';
import { IMatch } from '../models/match';
import { IStandings } from '../models/season';
import { RootStore } from './rootStore';
import { history } from '../..';

export default class SeasonStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);

    reaction(
      () => this.predicate.keys(),
      () => {
        this.matchRegistry.clear();
        this.loadMatches(this.standings?.id!);
      }
    );
  }

  seasonRegistry = new Map();
  standingsRegistry = new Map();
  matchRegistry = new Map();
  standings: IStandings | null = null;
  loading = false;
  submitting = false;
  error: AxiosResponse | null = null;
  predicate = new Map();

  get seasons() {
    return Array.from(this.seasonRegistry.values());
  }

  get matches(): IMatch[] {
    return Array.from(this.matchRegistry.values());
  }

  get axiosParams() {
    const params = new URLSearchParams();
    this.predicate.forEach((value, key) => {
      params.append(key, value);
    });

    return params;
  }

  setMatchesPredicate = (predicate: string, value: string) => {
    this.predicate.clear();
    this.predicate.set(predicate, value);
    history.push(`/seasons/${this.standings?.id}/results`);
  };

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

  loadMatches = async (id: number) => {
    this.loading = true;
    try {
      const matches = await agent.Seasons.matchesList(id, this.axiosParams);
      runInAction(() => {
        matches.forEach((match) => {
          this.matchRegistry.set(match.id, match);
        });
        this.loading = false;
        console.log(matches);
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
