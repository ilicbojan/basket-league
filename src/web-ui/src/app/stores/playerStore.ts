import { makeAutoObservable, reaction, runInAction } from 'mobx';
import agent from '../api/agent';
import {
  IPlayer,
  IPlayerAllTimeStats,
  IPlayerCurrentStats,
} from '../models/player';
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
  player: IPlayer | null = null;
  playerCurrentStats: IPlayerCurrentStats | null = null;
  playerAllTimeStats: IPlayerAllTimeStats | null = null;
  loadingPlayers = false;
  loadingCurrentStats = false;
  loadingAllTimeStats = false;
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

  loadPlayer = async (id: number) => {
    let player = this.getPlayer(id);
    if (player) {
      this.player = player;
    } else {
      this.loadingPlayers = true;
      try {
        player = await agent.Players.details(id);
        runInAction(() => {
          this.playerRegistry.set(player.id, player);
          this.player = player;
          this.loadingPlayers = false;
        });
      } catch (error) {
        runInAction(() => {
          this.loadingPlayers = false;
        });
        console.log(error);
      }
    }
  };

  getPlayer = (id: number): IPlayer => {
    return this.playerRegistry.get(id);
  };

  loadPlayerCurrentStats = async (id: number) => {
    this.loadingCurrentStats = true;
    try {
      const currentStats = await agent.Players.currentStats(id);
      runInAction(() => {
        this.playerCurrentStats = currentStats;
        this.loadingCurrentStats = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingCurrentStats = false;
      });
      console.log(error);
    }
  };

  loadPlayerAllTimeStats = async (id: number) => {
    this.loadingAllTimeStats = true;
    try {
      const allTimeStats = await agent.Players.allTimeStats(id);
      runInAction(() => {
        this.playerAllTimeStats = allTimeStats;
        this.loadingAllTimeStats = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingAllTimeStats = false;
      });
      console.log(error);
    }
  };
}
