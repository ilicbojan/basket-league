import { AxiosResponse } from 'axios';
import { action, computed, observable, runInAction } from 'mobx';
import { toast } from 'react-toastify';
import agent from '../api/agent';
import { ILeague } from '../models/league';
import { RootStore } from './rootStore';

export default class LeagueStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }

  @observable leagueRegistry = new Map();
  @observable loading = false;
  @observable submitting = false;
  @observable error: AxiosResponse | null = null;

  @computed get leagues() {
    return Array.from(this.leagueRegistry.values());
  }

  @action loadLeagues = async () => {
    this.loading = true;
    try {
      const leaguesVm = await agent.Leagues.list();
      const { leagues } = leaguesVm;
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

  @action createLeague = async (league: ILeague) => {
    this.submitting = true;
    try {
      league.id = await agent.Leagues.create(league);
      //get city and store it in league.city
      runInAction(() => {
        this.leagueRegistry.set(league.id, league);
        this.submitting = false;
      });
      toast.success('Uspesno ste kreirali ligu');
    } catch (error) {
      runInAction(() => {
        this.submitting = false;
        this.error = error;
      });
      console.log(error);
    }
  };
}
