import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { ITeam, ITeamStats } from '../models/team';
import { RootStore } from './rootStore';

export default class TeamStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }

  teamRegistry = new Map();
  team: ITeam | null = null;
  teamCurrentStats: ITeamStats | null = null;
  teamAllTimeStats: ITeamStats | null = null;
  loading = false;
  loadingCurrentStats = false;
  loadingAllTimeStats = false;

  get teams() {
    return Array.from(this.teamRegistry.values());
  }

  loadTeam = async (id: number): Promise<ITeam> => {
    let team = this.getTeam(id);
    if (team) {
      this.team = team;
      return team;
    } else {
      this.loading = true;
      try {
        team = await agent.Teams.details(id);
        runInAction(() => {
          this.teamRegistry.set(team.id, team);
          this.team = team;
          this.loading = false;
        });
      } catch (error) {
        runInAction(() => {
          this.loading = false;
        });
        console.log(error);
      }
      return team;
    }
  };

  getTeam = (id: number): ITeam => {
    return this.teamRegistry.get(id);
  };

  loadTeamCurrentStats = async (id: number) => {
    this.loadingCurrentStats = true;
    try {
      const currentStats = await agent.Teams.currentStats(id);
      runInAction(() => {
        this.teamCurrentStats = currentStats;
        this.loadingCurrentStats = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loadingCurrentStats = false;
      });
      console.log(error);
    }
  };

  loadTeamAllTimeStats = async (id: number) => {
    this.loadingAllTimeStats = true;
    try {
      const allTimeStats = await agent.Teams.allTimeStats(id);
      runInAction(() => {
        this.teamAllTimeStats = allTimeStats;
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
