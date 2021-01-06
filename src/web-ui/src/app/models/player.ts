export interface IPlayersVm {
  players: IPlayer[];
}

export interface IPlayer {
  id: number;
  jerseyNumber: number;
  firstName: string;
  lastName: string;
}
