import { configure } from 'mobx';
import { createContext } from 'react';
import CommonStore from './commonStore';
import LeagueStore from './leagueStore';
import ModalStore from './modalStore';
import SeasonStore from './seasonStore';

configure({ enforceActions: 'always' });

export class RootStore {
  commonStore: CommonStore;
  leagueStore: LeagueStore;
  modalStore: ModalStore;
  seasonStore: SeasonStore;

  constructor() {
    this.commonStore = new CommonStore(this);
    this.leagueStore = new LeagueStore(this);
    this.modalStore = new ModalStore(this);
    this.seasonStore = new SeasonStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
