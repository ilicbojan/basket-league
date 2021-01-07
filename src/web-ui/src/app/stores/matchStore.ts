import { AxiosResponse } from 'axios';
import { makeAutoObservable, reaction, runInAction } from 'mobx';
import agent from '../api/agent';
import { IMatch, IMatchStats } from '../models/match';
import { RootStore } from './rootStore';

export default class MatchStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);

    reaction(
      () => this.predicate.keys(),
      () => {
        this.matchRegistry.clear();
        this.loadMatches();
      }
    );
  }

  matchRegistry = new Map();
  match: IMatch | null = null;
  matchStats: IMatchStats | null = null;
  loadingMatches = false;
  loadingMatchStats = false;
  submitting = false;
  error: AxiosResponse | null = null;
  predicate = new Map();

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

  setMatchPredicate = (predicate: string, value: string) => {
    this.predicate.clear();
    this.predicate.set(predicate, value);
  };

  setMatchPredicates = (values: any) => {
    this.predicate.clear();
    for (var key in values) {
      this.predicate.set(key, values[key]);
    }
  };

  loadMatches = async () => {
    this.loadingMatches = true;
    try {
      const matches = await agent.Matches.list(this.axiosParams);
      runInAction(() => {
        matches.forEach((match) => {
          this.matchRegistry.set(match.id, match);
        });
        this.loadingMatches = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingMatches = false;
        this.error = error;
      });
      console.log(error);
    }
  };

  loadMatch = async (id: number): Promise<number> => {
    let match: IMatch = this.getMatch(id);
    if (match) {
      this.match = match;
      return match.seasonId;
    } else {
      this.loadingMatches = true;
      try {
        match = await agent.Matches.details(id);
        runInAction(() => {
          this.matchRegistry.set(match.id, match);
          this.match = match;
          this.loadingMatches = false;
        });
      } catch (error) {
        runInAction(() => {
          this.loadingMatches = false;
        });
        console.log(error);
      }
      return match.seasonId;
    }
  };

  loadMatchStats = async (id: number) => {
    this.loadingMatchStats = true;
    try {
      const matchStats = await agent.Matches.stats(id);
      runInAction(() => {
        this.matchStats = matchStats;
        this.loadingMatchStats = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingMatchStats = false;
      });
      console.log(error);
    }
  };

  getMatch = (id: number): IMatch => {
    return this.matchRegistry.get(id);
  };
}
