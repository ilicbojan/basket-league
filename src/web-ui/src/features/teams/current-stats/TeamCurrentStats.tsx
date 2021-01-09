import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { RootStoreContext } from '../../../app/stores/rootStore';

const TeamCurrentStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { teamCurrentStats, loadingCurrentStats } = rootStore.teamStore;

  return (
    <div>
      <div>{teamCurrentStats?.matchesPlayed}</div>
      <div>{teamCurrentStats?.scoredPoints}</div>
      <div>{teamCurrentStats?.receivedPoints}</div>
      <div>{teamCurrentStats?.pointsDiff}</div>
      <div>{teamCurrentStats?.assists}</div>
      <div>{teamCurrentStats?.fouls}</div>
      <div>{teamCurrentStats?.wins}</div>
      <div>{teamCurrentStats?.losses}</div>
      <div>{teamCurrentStats?.scoredPointsAvg}</div>
      <div>{teamCurrentStats?.receivedPointsAvg}</div>
      <div>{teamCurrentStats?.assistsAvg}</div>
      <div>{teamCurrentStats?.foulsAvg}</div>
      <div>{teamCurrentStats?.winsPercentage}%</div>
      <div>{teamCurrentStats?.lossesPercentage}%</div>
    </div>
  );
});

export default TeamCurrentStats;
