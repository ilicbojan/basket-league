import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { RootStoreContext } from '../../../app/stores/rootStore';

const PlayerAllTimeStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { playerAllTimeStats, loadingAllTimeStats } = rootStore.playerStore;

  return (
    <div>
      <div>{playerAllTimeStats?.matchesPlayed}</div>
      <div>{playerAllTimeStats?.pointsAvg}</div>
      <div>{playerAllTimeStats?.assistsAvg}</div>
      <div>{playerAllTimeStats?.foulsAvg}</div>
      <div>{playerAllTimeStats?.points}</div>
      <div>{playerAllTimeStats?.assists}</div>
      <div>{playerAllTimeStats?.fouls}</div>
    </div>
  );
});

export default PlayerAllTimeStats;
