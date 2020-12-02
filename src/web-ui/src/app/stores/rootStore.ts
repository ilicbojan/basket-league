import { configure } from 'mobx';
import { createContext } from 'react';
import CommonStore from './commonStore';
import LeagueStore from './leagueStore';
import ModalStore from './modalStore';

configure({ enforceActions: 'always' });

export class RootStore {
  commonStore: CommonStore;
  leagueStore: LeagueStore;
  modalStore: ModalStore;

  constructor() {
    this.commonStore = new CommonStore(this);
    this.leagueStore = new LeagueStore(this);
    this.modalStore = new ModalStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
