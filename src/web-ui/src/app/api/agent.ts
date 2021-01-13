import axios, { AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { history } from '../..';
import { ILeague, ILeaguesVm } from '../models/league';
import { ILineup } from '../models/lineup';
import { IH2HMatchesVm, IMatch, IMatchStats } from '../models/match';
import { IPlayer, IPlayerCurrentStats, IPlayersVm } from '../models/player';
import {
  IPlayersStats,
  IPlayerStats,
  ISeason,
  IStandings,
} from '../models/season';
import { ITeam, ITeamStats } from '../models/team';

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.request.use(
  (config: any) => {
    const token = window.localStorage.getItem('jwt');
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  (error: any) => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, (error: any) => {
  if (error.message === 'Network Error' && !error.response) {
    toast.error('Network error - make sure API is running!');
  }
  const { status, data, config, headers } = error.response;
  if (status === 404) {
    history.push('/notfound');
  }
  if (
    status === 401 &&
    headers['www-authenticate'].includes('Bearer error="invalid_token"')
  ) {
    window.localStorage.removeItem('jwt');
    history.push('/');
    toast.info('Your session has expired, please login again');
  }
  if (
    status === 400 &&
    config.method === 'get' &&
    data.errors.hasOwnProperty('id')
  ) {
    //history.push('/notfound');
  }
  if (status === 500) {
    toast.error('Server error - check the terminal for more info!');
  }
  throw error.response;
});

const responseBody = (response: AxiosResponse) => response.data;

// slow communication with API, use just in DEVELOPMENT
const sleep = (ms: number) => (response: AxiosResponse) =>
  new Promise<AxiosResponse>((resolve) =>
    setTimeout(() => resolve(response), ms)
  );

// remove sleep in PRODUCTION
const requests = {
  get: (url: string) => axios.get(url).then(sleep(1000)).then(responseBody),
  post: (url: string, body: {}) =>
    axios.post(url, body).then(sleep(1000)).then(responseBody),
  put: (url: string, body: {}) =>
    axios.put(url, body).then(sleep(1000)).then(responseBody),
  del: (url: string) => axios.delete(url).then(sleep(1000)).then(responseBody),
  postForm: (url: string, file: Blob) => {
    let formData = new FormData();
    formData.append('File', file);
    return axios
      .post(url, formData, {
        headers: { 'Content-type': 'multipart/form-data' },
      })
      .then(sleep(1000))
      .then(responseBody);
  },
};

const Leagues = {
  list: (): Promise<ILeaguesVm> => requests.get('/leagues'),
  standings: (id: number) => requests.get(`/leagues/${id}`),
  create: (league: ILeague) => requests.post('/leagues', league),
  update: (league: ILeague) => requests.put(`/leagues/${league.id}`, league),
  delete: (id: number) => requests.del(`/leagues/${id}`),
};

const Seasons = {
  list: (): Promise<ISeason[]> => requests.get('/seasons'),
  details: (id: number): Promise<ISeason> => requests.get(`/seasons/${id}`),
  standings: (id: number): Promise<IStandings> =>
    requests.get(`/seasons/${id}/standings`),
  playersStats: (id: number): Promise<IPlayersStats> =>
    requests.get(`/seasons/${id}/stats-players`),
};

const Matches = {
  list: (params: URLSearchParams): Promise<IMatch[]> =>
    axios.get(`/matches`, { params }).then(responseBody),
  details: (id: number): Promise<IMatch> => requests.get(`/matches/${id}`),
  stats: (id: number): Promise<IMatchStats> =>
    requests.get(`/matches/${id}/stats`),
  h2h: (id: number): Promise<IH2HMatchesVm> =>
    requests.get(`/matches/${id}/h2h`),
};

const MatchPlayers = {
  lineup: (id: number): Promise<ILineup> =>
    requests.get(`/matches/${id}/lineup`),
};

const Teams = {
  details: (id: number): Promise<ITeam> => requests.get(`/teams/${id}`),
  currentStats: (id: number): Promise<ITeamStats> =>
    requests.get(`/teams/${id}/current-stats`),
  allTimeStats: (id: number): Promise<ITeamStats> =>
    requests.get(`/teams/${id}/all-time-stats`),
};

const Players = {
  list: (params: URLSearchParams): Promise<IPlayersVm> =>
    axios.get(`/players`, { params }).then(responseBody),
  details: (id: number): Promise<IPlayer> => requests.get(`/players/${id}`),
  currentStats: (id: number): Promise<IPlayerCurrentStats> =>
    requests.get(`/players/${id}/current-stats`),
  allTimeStats: (id: number): Promise<IPlayerStats> =>
    requests.get(`/players/${id}/all-time-stats`),
};

const agent = {
  Leagues,
  Seasons,
  Matches,
  MatchPlayers,
  Teams,
  Players,
};

export default agent;
