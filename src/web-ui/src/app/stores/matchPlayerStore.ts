import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import { toast } from 'react-toastify';
import { history } from '../..';
import agent from '../api/agent';
import { ILineupFormValues, IMatchPlayer } from '../models/lineup';
import { RootStore } from './rootStore';

export default class MatchPlayerStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }

  lineupRegistry = new Map();
  loadingLineup = false;
  submitting = false;
  error: AxiosResponse | null = null;

  get lineup(): IMatchPlayer[] {
    return Array.from(this.lineupRegistry.values());
  }

  loadLineup = async (matchId: number) => {
    this.loadingLineup = true;
    try {
      const lineup = await agent.MatchPlayers.lineup(matchId);
      const { players } = lineup;
      runInAction(() => {
        this.lineupRegistry.clear();
        players.forEach((player) => {
          this.lineupRegistry.set(player.id, player);
        });
        this.loadingLineup = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingLineup = false;
      });
      console.log(error);
    }
  };

  createLineup = async (lineup: ILineupFormValues) => {
    this.submitting = true;
    try {
      await agent.MatchPlayers.create(lineup);
      this.loadLineup(lineup.matchId);
      runInAction(() => {
        this.submitting = false;
      });
      history.goBack();
      toast.success('Lineup created successfully');
    } catch (error) {
      runInAction(() => {
        this.submitting = false;
        this.error = error;
      });
      console.log(error);
    }
  };
}
