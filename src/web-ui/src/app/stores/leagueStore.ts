import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import { toast } from 'react-toastify';
import agent from '../api/agent';
import { ILeague } from '../models/league';
import { RootStore } from './rootStore';

export default class LeagueStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }

  leagueRegistry = new Map();
  loading = false;
  submitting = false;
  error: AxiosResponse | null = null;

  get leagues(): ILeague[] {
    return Array.from(this.leagueRegistry.values());
  }

  loadLeagues = async () => {
    this.loading = true;
    try {
      const { leagues } = await agent.Leagues.list();
      runInAction(() => {
        leagues.forEach((league) => {
          this.leagueRegistry.set(league.id, league);
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

  createLeague = async (league: ILeague) => {
    this.submitting = true;
    try {
      league.id = await agent.Leagues.create(league);
      //get city and store it in league.city
      runInAction(() => {
        this.leagueRegistry.set(league.id, league);
        this.submitting = false;
      });
      toast.success('League created successfully');
    } catch (error) {
      runInAction(() => {
        this.submitting = false;
        this.error = error;
      });
      console.log(error);
    }
  };
}
