import { AxiosResponse } from 'axios';
import { makeAutoObservable, runInAction } from 'mobx';
import { toast } from 'react-toastify';
import agent from '../api/agent';
import { ITeam, ITeamAllTimeStats, ITeamStats } from '../models/team';
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
  teamAllTimeStats: ITeamAllTimeStats | null = null;
  loading = false;
  submitting = false;
  loadingCurrentStats = false;
  loadingAllTimeStats = false;
  error: AxiosResponse | null = null;

  get teams(): ITeam[] {
    return Array.from(this.teamRegistry.values());
  }

  loadTeams = async () => {
    this.loading = true;
    try {
      const { teams } = await agent.Teams.list();
      runInAction(() => {
        teams.forEach((team) => {
          this.teamRegistry.set(team, team);
        });
        this.loading = false;
      });
    } catch (error) {
      runInAction(() => {
        this.loading = false;
      });
      console.log(error);
    }
  };

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

  createTeam = async (team: ITeam) => {
    this.submitting = true;
    try {
      team.id = await agent.Teams.create(team);
      runInAction(() => {
        this.teamRegistry.set(team.id, team);
        this.submitting = false;
      });
      toast.success('Team created successfully');
    } catch (error) {
      runInAction(() => {
        this.submitting = false;
        this.error = error;
      });
      console.log(error);
    }
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
