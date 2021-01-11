import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { RootStoreContext } from '../../../app/stores/rootStore';

const TeamAllTimeStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { teamAllTimeStats, loadingAllTimeStats } = rootStore.teamStore;

  return (
    <div>
      <div>{teamAllTimeStats?.matchesPlayed}</div>
      <div>{teamAllTimeStats?.scoredPoints}</div>
      <div>{teamAllTimeStats?.receivedPoints}</div>
      <div>{teamAllTimeStats?.pointsDiff}</div>
      <div>{teamAllTimeStats?.assists}</div>
      <div>{teamAllTimeStats?.fouls}</div>
      <div>{teamAllTimeStats?.wins}</div>
      <div>{teamAllTimeStats?.losses}</div>
      <div>{teamAllTimeStats?.scoredPointsAvg}</div>
      <div>{teamAllTimeStats?.receivedPointsAvg}</div>
      <div>{teamAllTimeStats?.assistsAvg}</div>
      <div>{teamAllTimeStats?.foulsAvg}</div>
      <div>{teamAllTimeStats?.winsPercentage}%</div>
      <div>{teamAllTimeStats?.lossesPercentage}%</div>
    </div>
  );
});

export default TeamAllTimeStats;
