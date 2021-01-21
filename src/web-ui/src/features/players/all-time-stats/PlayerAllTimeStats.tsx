import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './PlayerAllTimeStats.style';

const PlayerAllTimeStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { playerAllTimeStats, loadingAllTimeStats } = rootStore.playerStore;

  return (
    <S.PlayerAllTimeStats>
      <Table>
        <thead>
          <tr>
            <th></th>
            <th>MP</th>
            <th>PTS</th>
            <th>AST</th>
            <th>PF</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Totals</td>
            <td>{playerAllTimeStats?.matchesPlayed}</td>
            <td>{playerAllTimeStats?.points}</td>
            <td>{playerAllTimeStats?.assists}</td>
            <td>{playerAllTimeStats?.fouls}</td>
          </tr>
          <tr>
            <td>Averages</td>
            <td>{playerAllTimeStats?.matchesPlayed}</td>
            <td>{playerAllTimeStats?.pointsAvg}</td>
            <td>{playerAllTimeStats?.assistsAvg}</td>
            <td>{playerAllTimeStats?.foulsAvg}</td>
          </tr>
        </tbody>
      </Table>

      <Table>
        <thead>
          <tr>
            <th>Year</th>
            <th>League</th>
            <th>MP</th>
            <th>PTS</th>
            <th>AVG</th>
            <th>AST</th>
            <th>AVG</th>
            <th>PF</th>
            <th>AVG</th>
          </tr>
        </thead>
        <tbody>
          {playerAllTimeStats?.seasons.map((season) => (
            <tr key={season.season.id}>
              <td>{season.season.year}</td>
              <td>{season.season.name}</td>
              <td>{season.matchesPlayed}</td>
              <td>{season.points}</td>
              <td>{season.pointsAvg}</td>
              <td>{season.assists}</td>
              <td>{season.assistsAvg}</td>
              <td>{season.fouls}</td>
              <td>{season.foulsAvg}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </S.PlayerAllTimeStats>
  );
});

export default PlayerAllTimeStats;
