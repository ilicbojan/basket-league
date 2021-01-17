import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { Style } from '../../../style';

const PlayerAllTimeStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { playerAllTimeStats, loadingAllTimeStats } = rootStore.playerStore;

  return (
    <div>
      <Style.Stats>
        <div>
          <h6>Points AVG</h6>
          <span>{playerAllTimeStats?.pointsAvg}</span>
        </div>
        <div>
          <h6>Assists AVG</h6>
          <span>{playerAllTimeStats?.assistsAvg}</span>
        </div>
        <div>
          <h6>Fouls AVG</h6>
          <span>{playerAllTimeStats?.foulsAvg}</span>
        </div>
        <div>
          <h6>Points</h6>
          <span>{playerAllTimeStats?.points}</span>
        </div>
        <div>
          <h6>Assists</h6>
          <span>{playerAllTimeStats?.assists}</span>
        </div>
        <div>
          <h6>Fouls</h6>
          <span>{playerAllTimeStats?.fouls}</span>
        </div>
        <div>
          <h6>Matches</h6>
          <span>{playerAllTimeStats?.matchesPlayed}</span>
        </div>
      </Style.Stats>

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
