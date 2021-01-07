import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';

const MatchStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { loadingMatchStats, matchStats } = rootStore.matchStore;

  if (loadingMatchStats) return <LoadingSpinner />;

  return (
    <div>
      <div>
        {matchStats?.homeTeam.name} - {matchStats?.awayTeam.name}
      </div>
      <div>
        {matchStats?.homePoints} POINTS {matchStats?.awayPoints}
      </div>
      <div>
        {matchStats?.homeAssists} ASSISTS {matchStats?.awayAssists}
      </div>
      <div>
        {matchStats?.homeFouls} FOULS {matchStats?.awayFouls}
      </div>
    </div>
  );
});

export default MatchStats;
