import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { Style } from '../../../style';

const TeamAllTimeStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { teamAllTimeStats, loadingAllTimeStats } = rootStore.teamStore;

  return (
    <div>
      <Style.Stats>
        <div>
          <h6>Matches</h6>
          <span>{teamAllTimeStats?.matchesPlayed}</span>
        </div>
        <div>
          <h6>Points+</h6>
          <span>{teamAllTimeStats?.scoredPoints}</span>
        </div>
        <div>
          <h6>Points-</h6>
          <span>{teamAllTimeStats?.receivedPoints}</span>
        </div>
        <div>
          <h6>Points +/-</h6>
          <span>{teamAllTimeStats?.pointsDiff}</span>
        </div>
        <div>
          <h6>Assists</h6>
          <span>{teamAllTimeStats?.assists}</span>
        </div>
        <div>
          <h6>Fouls</h6>
          <span>{teamAllTimeStats?.fouls}</span>
        </div>
        <div>
          <h6>Wins</h6>
          <span>{teamAllTimeStats?.wins}</span>
        </div>
        <div>
          <h6>Losses</h6>
          <span>{teamAllTimeStats?.losses}</span>
        </div>
        <div>
          <h6>Points+ AVG</h6>
          <span>{teamAllTimeStats?.scoredPointsAvg}</span>
        </div>
        <div>
          <h6>Points- AVG</h6>
          <span>{teamAllTimeStats?.receivedPointsAvg}</span>
        </div>
        <div>
          <h6>Assists AVG</h6>
          <span>{teamAllTimeStats?.assistsAvg}</span>
        </div>
        <div>
          <h6>Fouls AVG</h6>
          <span>{teamAllTimeStats?.foulsAvg}</span>
        </div>
        <div>
          <h6>Wins %</h6>
          <span>{teamAllTimeStats?.winsPercentage}%</span>
        </div>
        <div>
          <h6>Losses %</h6>
          <span>{teamAllTimeStats?.lossesPercentage}%</span>
        </div>
      </Style.Stats>

      <Table>
        <thead>
          <tr>
            <th>Year</th>
            <th>League</th>
            <th>MP</th>
            <th>W</th>
            <th>L</th>
            <th>P+</th>
            <th>P-</th>
            <th>A</th>
            <th>F</th>
          </tr>
        </thead>
        <tbody>
          {teamAllTimeStats?.seasons.map((season) => (
            <tr key={season.id}>
              <td>{season.year}</td>
              <td>{season.name}</td>
              <td>{season.matchesPlayed}</td>
              <td>{season.wins}</td>
              <td>{season.losses}</td>
              <td>{season.scoredPointsAvg}</td>
              <td>{season.receivedPointsAvg}</td>
              <td>{season.assistsAvg}</td>
              <td>{season.foulsAvg}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
});

export default TeamAllTimeStats;
