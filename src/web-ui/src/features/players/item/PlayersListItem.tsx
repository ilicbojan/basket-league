import { observer } from 'mobx-react-lite';
import React from 'react';
import { Link } from 'react-router-dom';
import { IPlayer } from '../../../app/models/player';
import { S } from './PlayersListItem.style';

interface IProps {
  player: IPlayer;
}

const PlayersListItem: React.FC<IProps> = observer(({ player }) => {
  return (
    <S.PlayersListItem>
      <Link to={`/players/${player.id}`}>
        <img src='/images/user.jpg' alt='' className='image' />
        <div className='info'>
          {player.firstName} {player.lastName}
          <div>#{player.jerseyNumber}</div>
        </div>
      </Link>
    </S.PlayersListItem>
  );
});

export default PlayersListItem;
