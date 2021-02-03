import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { getDate, getTime } from '../../../app/common/util/dates';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './MatchInfo.style';

const MatchInfo = () => {
  const rootStore = useContext(RootStoreContext);
  const { match } = rootStore.matchStore;

  return (
    <S.MatchInfo>
      <div className='league'>League - Round {match?.round}</div>
      <div className='match'>
        <div className='team'>
          <Link to={`/teams/${match?.homeTeam.id}`}>
            <img className='image' src='/images/team.jpg' />
          </Link>
          <div className='name'>
            <Link to={`/teams/${match?.homeTeam.id}`}>
              {match?.homeTeam.name}
            </Link>
          </div>
        </div>
        <div className='info'>
          <div className='date'>
            {getDate(match?.date!)} {getTime(match?.time!)}
          </div>
          <div className='score'>
            {match?.homePoints} <span>-</span> {match?.awayPoints}
          </div>
        </div>
        <div className='team'>
          <Link to={`/teams/${match?.awayTeam.id}`}>
            <img className='image' src='/images/team.jpg' />
          </Link>
          <div className='name'>
            <Link to={`/teams/${match?.awayTeam.id}`}>
              {match?.awayTeam.name}
            </Link>
          </div>
        </div>
      </div>
    </S.MatchInfo>
  );
};

export default MatchInfo;
