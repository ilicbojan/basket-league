import { makeAutoObservable, reaction, runInAction } from 'mobx';
import agent from '../api/agent';
import { IPlayer } from '../models/player';
import { RootStore } from './rootStore';

export default class PlayerStore {
  rootStore: RootStore;
  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);

    reaction(
      () => this.predicate.keys(),
      () => {
        this.playerRegistry.clear();
        this.loadPlayers();
      }
    );
  }

  playerRegistry = new Map();
  loadingPlayers = false;
  predicate = new Map();

  get players(): IPlayer[] {
    return Array.from(this.playerRegistry.values());
  }

  get axiosParams() {
    const params = new URLSearchParams();
    this.predicate.forEach((value, key) => {
      params.append(key, value);
    });

    return params;
  }

  setPredicate = (predicate: string, value: string) => {
    this.predicate.clear();
    this.predicate.set(predicate, value);
  };

  loadPlayers = async () => {
    this.loadingPlayers = true;
    try {
      const { players } = await agent.Players.list(this.axiosParams);
      runInAction(() => {
        players.forEach((player) => {
          this.playerRegistry.set(player.id, player);
        });
        this.loadingPlayers = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingPlayers = false;
      });
      console.log(error);
    }
  };
}
