import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import PlayersListItem from '../item/PlayersListItem';
import { S } from './PlayersList.style';

interface IProps {
  teamId?: number;
}

const PlayersList: React.FC<IProps> = observer(({ teamId }) => {
  const rootStore = useContext(RootStoreContext);
  const { setPredicate, players, loadingPlayers } = rootStore.playerStore;

  useEffect(() => {
    setPredicate('teamId', teamId + '');
  }, [setPredicate, teamId]);

  if (loadingPlayers) return <LoadingSpinner />;

  return (
    <S.PlayersList>
      {players.map((player) => (
        <PlayersListItem key={player.id} player={player} />
      ))}
    </S.PlayersList>
  );
});

export default PlayersList;
