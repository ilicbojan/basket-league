import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import { RootStoreContext } from '../../../app/stores/rootStore';

const PlayerCurrentStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { playerCurrentStats, loadingCurrentStats } = rootStore.playerStore;

  return (
    <div>
      <div>{playerCurrentStats?.matchesPlayed}</div>
      <div>{playerCurrentStats?.pointsAvg}</div>
      <div>{playerCurrentStats?.assistsAvg}</div>
      <div>{playerCurrentStats?.foulsAvg}</div>
      <div>{playerCurrentStats?.points}</div>
      <div>{playerCurrentStats?.assists}</div>
      <div>{playerCurrentStats?.fouls}</div>

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
    </div>
  );
});

export default PlayerCurrentStats;
