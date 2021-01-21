import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './PlayerCurrentStats.style';

const PlayerCurrentStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { playerCurrentStats, loadingCurrentStats } = rootStore.playerStore;

  return (
    <S.PlayerCurrentStats>
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
            <td>{playerCurrentStats?.matchesPlayed}</td>
            <td>{playerCurrentStats?.points}</td>
            <td>{playerCurrentStats?.assists}</td>
            <td>{playerCurrentStats?.fouls}</td>
          </tr>
          <tr>
            <td>Averages</td>
            <td>{playerCurrentStats?.matchesPlayed}</td>
            <td>{playerCurrentStats?.pointsAvg}</td>
            <td>{playerCurrentStats?.assistsAvg}</td>
            <td>{playerCurrentStats?.foulsAvg}</td>
          </tr>
        </tbody>
      </Table>

      <Table>
        <thead>
          <tr>
            <th>#</th>
            <th>Team</th>
            <th>Points</th>
            <th>Assists</th>
            <th>Fouls</th>
          </tr>
        </thead>
        <tbody>
          {playerCurrentStats?.matches.map((match, i) => (
            <tr key={i}>
              <td>{i + 1}</td>
              <td>{match.team.name}</td>
              <td>{match.points}</td>
              <td>{match.assists}</td>
              <td>{match.fouls}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </S.PlayerCurrentStats>
  );
});

export default PlayerCurrentStats;
