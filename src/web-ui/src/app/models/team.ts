export interface ITeam {
  id: number;
  name: string;
  currentSeasonId: number;
}

export interface ITeamStats {
  matchesPlayed: number;
  scoredPoints: number;
  receivedPoints: number;
  pointsDiff: number;
  assists: number;
  fouls: number;
  wins: number;
  losses: number;
  scoredPointsAvg: number;
  receivedPointsAvg: number;
  assistsAvg: number;
  foulsAvg: number;
  winsPercentage: number;
  lossesPercentage: number;
}
