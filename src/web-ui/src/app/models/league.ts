export interface ILeaguesVm {
  leagues: ILeague[];
}

export interface ILeague {
  id: number;
  name: string;
  city: ICity;
}

interface ICity {
  id: number;
  name: string;
  country: ICountry;
}

interface ICountry {
  id: number;
  name: string;
}
