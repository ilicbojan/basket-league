import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';

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
    <div>
      {players.map((player) => (
        <div key={player.id}>
          <div>{player.jerseyNumber}</div>
          <div>{player.firstName}</div>
          <div>{player.lastName}</div>
        </div>
      ))}
    </div>
  );
});

export default PlayersList;
