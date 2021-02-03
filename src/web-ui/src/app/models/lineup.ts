export interface ILineup {
  players: IMatchPlayer[];
}

export interface IMatchPlayer {
  id: number;
  firstName: string;
  lastName: string;
  points: number;
  assists: number;
  fouls: number;
  team: ITeam;
}

interface ITeam {
  id: number;
  name: string;
}

export interface ILineupFormValues {
  matchId: number;
  teamId: number;
  playersIds: number[];
}
