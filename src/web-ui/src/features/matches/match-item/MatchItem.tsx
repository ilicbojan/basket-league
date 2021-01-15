import { observer } from 'mobx-react-lite';
import React from 'react';
import { Link } from 'react-router-dom';
import { getDateAndMonth, getTime } from '../../../app/common/util/dates';
import { IMatch } from '../../../app/models/match';
import { S } from './MatchItem.style';

interface IProps {
  match: IMatch;
}

const MatchItem: React.FC<IProps> = observer(({ match }) => {
  return (
    <Link to={`/matches/${match.id}`}>
      <S.MatchItem>
        <div className='date'>
          {getDateAndMonth(match.date)} {getTime(match.time)}
        </div>
        <div>{match.homeTeam.name}</div>
        <div>{match.awayTeam.name}</div>
      </S.MatchItem>
    </Link>
  );
});

export default MatchItem;
