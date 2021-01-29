import { configure } from 'mobx';
import { createContext } from 'react';
import CityStore from './cityStore';
import CommonStore from './commonStore';
import LeagueStore from './leagueStore';
import MatchPlayerStore from './matchPlayerStore';
import MatchStore from './matchStore';
import ModalStore from './modalStore';
import PlayerStore from './playerStore';
import SeasonStore from './seasonStore';
import TeamStore from './teamStore';

configure({ enforceActions: 'always' });

export class RootStore {
  commonStore: CommonStore;
  leagueStore: LeagueStore;
  modalStore: ModalStore;
  seasonStore: SeasonStore;
  matchStore: MatchStore;
  matchPlayerStore: MatchPlayerStore;
  teamStore: TeamStore;
  playerStore: PlayerStore;
  cityStore: CityStore;

  constructor() {
    this.commonStore = new CommonStore(this);
    this.leagueStore = new LeagueStore(this);
    this.modalStore = new ModalStore(this);
    this.seasonStore = new SeasonStore(this);
    this.matchStore = new MatchStore(this);
    this.matchPlayerStore = new MatchPlayerStore(this);
    this.teamStore = new TeamStore(this);
    this.playerStore = new PlayerStore(this);
    this.cityStore = new CityStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
