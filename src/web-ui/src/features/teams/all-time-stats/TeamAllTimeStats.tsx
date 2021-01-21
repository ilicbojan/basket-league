import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './TeamAllTimeStats.style';

const TeamAllTimeStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { teamAllTimeStats, loadingAllTimeStats } = rootStore.teamStore;

  return (
    <S.TeamAllTimeStats>
      <Table>
        <thead>
          <tr>
            <th></th>
            <th>MP</th>
            <th>PTS+</th>
            <th>PTS-</th>
            <th>AST</th>
            <th>PF</th>
            <th>W</th>
            <th>L</th>
            <th>PTS+/-</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Totals</td>
            <td>{teamAllTimeStats?.matchesPlayed}</td>
            <td>{teamAllTimeStats?.scoredPoints}</td>
            <td>{teamAllTimeStats?.receivedPoints}</td>
            <td>{teamAllTimeStats?.assists}</td>
            <td>{teamAllTimeStats?.fouls}</td>
            <td>{teamAllTimeStats?.wins}</td>
            <td>{teamAllTimeStats?.losses}</td>
            <td>{teamAllTimeStats?.pointsDiff}</td>
          </tr>
          <tr>
            <td>AVG</td>
            <td>{teamAllTimeStats?.matchesPlayed}</td>
            <td>{teamAllTimeStats?.scoredPointsAvg}</td>
            <td>{teamAllTimeStats?.receivedPointsAvg}</td>
            <td>{teamAllTimeStats?.assistsAvg}</td>
            <td>{teamAllTimeStats?.foulsAvg}</td>
            <td>{teamAllTimeStats?.winsPercentage}%</td>
            <td>{teamAllTimeStats?.lossesPercentage}%</td>
          </tr>
        </tbody>
      </Table>

      <Table>
        <thead>
          <tr>
            <th>Year</th>
            <th>League</th>
            <th>MP</th>
            <th>PTS+</th>
            <th>AVG</th>
            <th>PTS-</th>
            <th>AVG</th>
            <th>AST</th>
            <th>AVG</th>
            <th>PF</th>
            <th>AVG</th>
            <th>W</th>
            <th>L</th>
          </tr>
        </thead>
        <tbody>
          {teamAllTimeStats?.seasons.map((season) => (
            <tr key={season.id}>
              <td>{season.year}</td>
              <td>{season.name}</td>
              <td>{season.matchesPlayed}</td>
              <td>{season.scoredPoints}</td>
              <td>{season.scoredPointsAvg}</td>
              <td>{season.receivedPoints}</td>
              <td>{season.receivedPointsAvg}</td>
              <td>{season.assists}</td>
              <td>{season.assistsAvg}</td>
              <td>{season.fouls}</td>
              <td>{season.foulsAvg}</td>
              <td>{season.wins}</td>
              <td>{season.losses}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </S.TeamAllTimeStats>
  );
});

export default TeamAllTimeStats;
