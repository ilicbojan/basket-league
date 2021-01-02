import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';

const MatchLineup = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { loadingLineup, lineup } = rootStore.matchPlayerStore;

  if (loadingLineup) return <LoadingSpinner />;

  return (
    <div>
      <div>Lineup and stats</div>
      {lineup.map((player) => (
        <div key={player.id}>
          <div>{player.team.name}</div>
          <div>{player.firstName}</div>
          <div>{player.lastName}</div>
          <div>{player.points}</div>
          <div>{player.assists}</div>
          <div>{player.fouls}</div>
        </div>
      ))}
    </div>
  );
});

export default MatchLineup;
