import { configure } from 'mobx';
import { createContext } from 'react';
import CommonStore from './commonStore';

configure({ enforceActions: 'always' });

export class RootStore {
  commonStore: CommonStore;

  constructor() {
    this.commonStore = new CommonStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
