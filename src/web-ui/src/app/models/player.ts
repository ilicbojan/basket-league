export interface IPlayersVm {
  players: IPlayer[];
}

export interface IPlayer {
  id: number;
  jerseyNumber: number;
  firstName: string;
  lastName: string;
  team: ITeam;
}
export interface IPlayerCurrentStats {
  matchesPlayed: number;
  pointsAvg: number;
  assistsAvg: number;
  foulsAvg: number;
  points: number;
  assists: number;
  fouls: number;
  matches: IMatch[];
}

interface IMatch {
  points: number;
  assists: number;
  fouls: number;
  team: ITeam;
}

interface ITeam {
  id: number;
  name: string;
}

export interface IPlayerAllTimeStats {
  matchesPlayed: number;
  pointsAvg: number;
  assistsAvg: number;
  foulsAvg: number;
  points: number;
  assists: number;
  fouls: number;
  seasons: ISeasonPlayer[];
}

export interface ISeasonPlayer {
  matchesPlayed: number;
  pointsAvg: number;
  assistsAvg: number;
  foulsAvg: number;
  points: number;
  assists: number;
  fouls: number;
  season: ISeason;
}

interface ISeason {
  id: number;
  name: string;
  year: number;
}
