import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './TeamCurrentStats.style';

const TeamCurrentStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { teamCurrentStats } = rootStore.teamStore;

  return (
    <S.TeamCurrentStats>
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
            <td>{teamCurrentStats?.matchesPlayed}</td>
            <td>{teamCurrentStats?.scoredPoints}</td>
            <td>{teamCurrentStats?.receivedPoints}</td>
            <td>{teamCurrentStats?.assists}</td>
            <td>{teamCurrentStats?.fouls}</td>
            <td>{teamCurrentStats?.wins}</td>
            <td>{teamCurrentStats?.losses}</td>
            <td>{teamCurrentStats?.pointsDiff}</td>
          </tr>
          <tr>
            <td>AVG</td>
            <td>{teamCurrentStats?.matchesPlayed}</td>
            <td>{teamCurrentStats?.scoredPointsAvg}</td>
            <td>{teamCurrentStats?.receivedPointsAvg}</td>
            <td>{teamCurrentStats?.assistsAvg}</td>
            <td>{teamCurrentStats?.foulsAvg}</td>
            <td>{teamCurrentStats?.winsPercentage}%</td>
            <td>{teamCurrentStats?.lossesPercentage}%</td>
            <td></td>
          </tr>
        </tbody>
      </Table>
    </S.TeamCurrentStats>
  );
});

export default TeamCurrentStats;
