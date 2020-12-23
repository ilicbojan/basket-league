export interface IMatch {
  id: number;
  date: Date;
  time: string;
  round: number;
  homePoints: number;
  awayPoints: number;
  homeTeam: ITeam;
  awayTeam: ITeam;
}

interface ITeam {
  id: number;
  name: string;
}
