export interface ISeason {
  id: number;
  name: string;
  year: number;
  league: ILeague;
}

interface ILeague {
  id: number;
  name: string;
}

export interface IStandings {
  id: number;
  name: string;
  year: number;
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
