import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { Style } from '../../../style';

const TeamCurrentStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { teamCurrentStats, loadingCurrentStats } = rootStore.teamStore;

  return (
    <div>
      <Style.Stats>
        <div>
          <h6>PTS+</h6>
          <span>{teamCurrentStats?.scoredPoints}</span>
        </div>
        <div>
          <h6>PTS-</h6>
          <span>{teamCurrentStats?.receivedPoints}</span>
        </div>
        <div>
          <h6>PTS +/-</h6>
          <span>{teamCurrentStats?.pointsDiff}</span>
        </div>
        <div>
          <h6>AST</h6>
          <span>{teamCurrentStats?.assists}</span>
        </div>
        <div>
          <h6>PF</h6>
          <span>{teamCurrentStats?.fouls}</span>
        </div>
        <div>
          <h6>PTS+ AVG</h6>
          <span>{teamCurrentStats?.scoredPointsAvg}</span>
        </div>
        <div>
          <h6>PTS- AVG</h6>
          <span>{teamCurrentStats?.receivedPointsAvg}</span>
        </div>
        <div>
          <h6>AST AVG</h6>
          <span>{teamCurrentStats?.assistsAvg}</span>
        </div>
        <div>
          <h6>PF AVG</h6>
          <span>{teamCurrentStats?.foulsAvg}</span>
        </div>
        <div>
          <h6>MP</h6>
          <span>{teamCurrentStats?.matchesPlayed}</span>
        </div>
        <div>
          <h6>W</h6>
          <span>{teamCurrentStats?.wins}</span>
        </div>
        <div>
          <h6>L</h6>
          <span>{teamCurrentStats?.losses}</span>
        </div>
        <div>
          <h6>W %</h6>
          <span>{teamCurrentStats?.winsPercentage}%</span>
        </div>
        <div>
          <h6>L %</h6>
          <span>{teamCurrentStats?.lossesPercentage}%</span>
        </div>
      </Style.Stats>
    </div>
  );
});

export default TeamCurrentStats;
