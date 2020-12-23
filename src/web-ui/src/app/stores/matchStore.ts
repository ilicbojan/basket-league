import { makeAutoObservable } from 'mobx';
import { RootStore } from './rootStore';

export default class MatchStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
  }
}
