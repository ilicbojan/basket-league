import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { Style } from '../../../style';

const PlayerCurrentStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { playerCurrentStats, loadingCurrentStats } = rootStore.playerStore;

  return (
    <div>
      <Style.Stats>
        <div>
          <h6>Points AVG</h6>
          <span>{playerCurrentStats?.pointsAvg}</span>
        </div>
        <div>
          <h6>Assists AVG</h6>
          <span>{playerCurrentStats?.assistsAvg}</span>
        </div>
        <div>
          <h6>Fouls AVG</h6>
          <span>{playerCurrentStats?.foulsAvg}</span>
        </div>
        <div>
          <h6>Points</h6>
          <span>{playerCurrentStats?.points}</span>
        </div>
        <div>
          <h6>Assists</h6>
          <span>{playerCurrentStats?.assists}</span>
        </div>
        <div>
          <h6>Fouls</h6>
          <span>{playerCurrentStats?.fouls}</span>
        </div>
        <div>
          <h6>Matches</h6>
          <span>{playerCurrentStats?.matchesPlayed}</span>
        </div>
      </Style.Stats>

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
