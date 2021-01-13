import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
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

      <Table>
        <thead>
          <tr>
            <th>Year</th>
            <th>League</th>
            <th>MP</th>
            <th>P AVG</th>
            <th>A AVG</th>
            <th>F AVG</th>
            <th>P</th>
            <th>A</th>
            <th>F</th>
          </tr>
        </thead>
        <tbody>
          {playerAllTimeStats?.seasons.map((season) => (
            <tr key={season.season.id}>
              <td>{season.season.year}</td>
              <td>{season.season.name}</td>
              <td>{season.matchesPlayed}</td>
              <td>{season.pointsAvg}</td>
              <td>{season.assistsAvg}</td>
              <td>{season.foulsAvg}</td>
              <td>{season.points}</td>
              <td>{season.assists}</td>
              <td>{season.fouls}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
});

export default PlayerAllTimeStats;
