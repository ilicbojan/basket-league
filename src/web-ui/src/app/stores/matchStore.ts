import { AxiosResponse } from 'axios';
import { makeAutoObservable, reaction, runInAction } from 'mobx';
import agent from '../api/agent';
import { IMatch } from '../models/match';
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
        this.loadMatches(this.seasonId!);
      }
    );
  }

  matchRegistry = new Map();
  match: IMatch | null = null;
  resultRegistry = new Map();
  seasonId: number | null = null;
  loadingMatches = false;
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

  setMatchesPredicate = (predicate: string, value: string) => {
    this.predicate.clear();
    this.predicate.set(predicate, value);
  };

  setSeasonId = (id: number) => {
    this.seasonId = id;
  };

  loadMatches = async (id: number) => {
    this.loadingMatches = true;
    try {
      const matches = await agent.Matches.list(id, this.axiosParams);
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

  getMatch = (id: number): IMatch => {
    return this.matchRegistry.get(id);
  };
}
