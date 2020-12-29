import { observer } from 'mobx-react-lite';
import React from 'react';
import { Link } from 'react-router-dom';
import { IMatch } from '../../../app/models/match';
import { S } from './MatchesListItem.style';

interface IProps {
  match: IMatch;
}

const MatchesListItem: React.FC<IProps> = observer(({ match }) => {
  return (
    <Link to={`/matches/${match.id}`}>
      <S.MatchesListItem>
        <div>{match.date}</div>
        <div>{match.time}</div>
        <div>{match.homeTeam.name}</div>
        <div>{match.homePoints}</div>
        <div>{match.awayPoints}</div>
        <div>{match.awayTeam.name}</div>
      </S.MatchesListItem>
    </Link>
  );
});

export default MatchesListItem;
