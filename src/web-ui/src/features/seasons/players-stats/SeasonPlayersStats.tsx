import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';

const SeasonPlayersStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { loadingPlayersStats, playersStats } = rootStore.seasonStore;

  return (
    <div>
      <Table>
        <thead>
          <tr>
            <th>#</th>
            <th>Player</th>
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
          {loadingPlayersStats ? (
            <tr>
              <td className='spin' colSpan={9}>
                <LoadingSpinner />
              </td>
            </tr>
          ) : (
            playersStats.map((player, i) => (
              <tr key={player.id}>
                <td>{i + 1}</td>
                <td>
                  {player.firstName} {player.lastName}
                </td>
                <td>{player.matchesPlayed}</td>
                <td>{player.pointsAvg}</td>
                <td>{player.assistsAvg}</td>
                <td>{player.foulsAvg}</td>
                <td>{player.points}</td>
                <td>{player.assists}</td>
                <td>{player.fouls}</td>
              </tr>
            ))
          )}
        </tbody>
      </Table>
    </div>
  );
});

export default SeasonPlayersStats;
