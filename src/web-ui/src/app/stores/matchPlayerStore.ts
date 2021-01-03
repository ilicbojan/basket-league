import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { IMatchPlayer } from '../models/lineup';
import { RootStore } from './rootStore';

export default class MatchPlayerStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }

  lineupRegistry = new Map();
  loadingLineup = false;

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
}
