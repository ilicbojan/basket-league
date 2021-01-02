import { configure } from 'mobx';
import { createContext } from 'react';
import CommonStore from './commonStore';
import LeagueStore from './leagueStore';
import MatchPlayerStore from './matchPlayerStore';
import MatchStore from './matchStore';
import ModalStore from './modalStore';
import SeasonStore from './seasonStore';

configure({ enforceActions: 'always' });

export class RootStore {
  commonStore: CommonStore;
  leagueStore: LeagueStore;
  modalStore: ModalStore;
  seasonStore: SeasonStore;
  matchStore: MatchStore;
  matchPlayerStore: MatchPlayerStore;

  constructor() {
    this.commonStore = new CommonStore(this);
    this.leagueStore = new LeagueStore(this);
    this.modalStore = new ModalStore(this);
    this.seasonStore = new SeasonStore(this);
    this.matchStore = new MatchStore(this);
    this.matchPlayerStore = new MatchPlayerStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
