import { observer } from 'mobx-react-lite';
import React from 'react';
import { Link } from 'react-router-dom';
import { getDateAndMonth } from '../../../app/common/util/dates';
import { IMatch } from '../../../app/models/match';
import { S } from './ResultItem.style';

interface IProps {
  match: IMatch;
}

const ResultItem: React.FC<IProps> = observer(({ match }) => {
  return (
    <Link to={`/matches/${match.id}`}>
      <S.ResultItem>
        <div className='date'>{getDateAndMonth(match.date)}</div>
        <div>{match.homeTeam.name}</div>
        <div>{match.awayTeam.name}</div>
        <div className='score'>
          <span>{match.homePoints}:</span>
          <span>{match.awayPoints}</span>
        </div>
      </S.ResultItem>
    </Link>
  );
});

export default ResultItem;
