import { AxiosResponse } from 'axios';
import { makeAutoObservable, reaction, runInAction } from 'mobx';
import { toast } from 'react-toastify';
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
  h2hRegistry = new Map();
  match: IMatch | null = null;
  matchStats: IMatchStats | null = null;
  loadingMatches = false;
  loadingMatchStats = false;
  loadingH2H = false;
  submitting = false;
  error: AxiosResponse | null = null;
  predicate = new Map();

  get matches(): IMatch[] {
    return Array.from(this.matchRegistry.values());
  }

  get h2h(): IMatch[] {
    return Array.from(this.h2hRegistry.values());
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
          match.date = new Date(match.date);
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
          match.date = new Date(match.date);
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

  createMatch = async (match: IMatch) => {
    this.submitting = true;
    try {
      match.id = await agent.Matches.create(match);
      runInAction(() => {
        this.matchRegistry.set(match.id, match);
        this.submitting = false;
      });
      toast.success('Match created successfully');
    } catch (error) {
      runInAction(() => {
        this.submitting = false;
        this.error = error;
      });
      console.log(error);
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

  loadH2HMatches = async (id: number) => {
    this.loadingH2H = true;
    try {
      const { matches } = await agent.Matches.h2h(id);
      runInAction(() => {
        matches.forEach((match) => {
          match.date = new Date(match.date);
          this.h2hRegistry.set(match.id, match);
        });
        this.loadingH2H = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingH2H = false;
      });
    }
  };

  getMatch = (id: number): IMatch => {
    return this.matchRegistry.get(id);
  };
}
