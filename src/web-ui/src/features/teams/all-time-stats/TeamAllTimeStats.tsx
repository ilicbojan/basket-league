import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
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
