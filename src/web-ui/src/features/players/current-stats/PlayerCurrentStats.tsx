import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { RootStoreContext } from '../../../app/stores/rootStore';

const PlayerCurrentStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { playerCurrentStats, loadingCurrentStats } = rootStore.playerStore;

  return (
    <div>
      <div>{playerCurrentStats?.matchesPlayed}</div>
      <div>{playerCurrentStats?.pointsAvg}</div>
      <div>{playerCurrentStats?.assistsAvg}</div>
      <div>{playerCurrentStats?.foulsAvg}</div>
      <div>{playerCurrentStats?.points}</div>
      <div>{playerCurrentStats?.assists}</div>
      <div>{playerCurrentStats?.fouls}</div>
    </div>
  );
});

export default PlayerCurrentStats;
