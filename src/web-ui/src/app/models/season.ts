export interface ISeasonsVm {
  seasons: ISeason[];
}

export interface ISeason {
  id: number;
  name: string;
  year: number;
  league: ILeague;
  field: IField;
}

interface ILeague {
  id: number;
  name: string;
}

interface IField {
  id: number;
  name: string;
}

export interface IStandings {
  seasonId: number;
  teams: ITeam[];
}

interface ITeam {
  id: number;
  name: string;
  matchesPlayed: number;
  wins: number;
  losses: number;
  scoredPoints: number;
  receivedPoints: number;
  pointsDiff: number;
  points: number;
}
export interface IPlayersStats {
  players: IPlayerStats[];
}

export interface IPlayerStats {
  id: number;
  firstName: string;
  lastName: string;
  matchesPlayed: number;
  pointsAvg: number;
  assistsAvg: number;
  foulsAvg: number;
  points: number;
  assists: number;
  fouls: number;
}
