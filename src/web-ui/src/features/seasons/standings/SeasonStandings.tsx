import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import Table from '../../../app/common/table/Table';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';

const SeasonStandings = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { standings, loadingStandings } = rootStore.seasonStore;

  return (
    <div>
      <Table>
        <thead>
          <tr>
            <th>#</th>
            <th>Team</th>
            <th>MP</th>
            <th>W</th>
            <th>L</th>
            <th>P+</th>
            <th>P-</th>
            <th>+/-</th>
            <th>P</th>
          </tr>
        </thead>
        <tbody>
          {loadingStandings ? (
            <tr>
              <td className='spin' colSpan={9}>
                <LoadingSpinner />
              </td>
            </tr>
          ) : (
            standings?.teams.map((team, i) => (
              <tr key={team.id}>
                <td>{i + 1}</td>
                <td>{team.name}</td>
                <td>{team.matchesPlayed}</td>
                <td>{team.wins}</td>
                <td>{team.losses}</td>
                <td>{team.scoredPoints}</td>
                <td>{team.receivedPoints}</td>
                <td>{team.pointsDiff}</td>
                <td>{team.points}</td>
              </tr>
            ))
          )}
        </tbody>
      </Table>
    </div>
  );
});

export default SeasonStandings;
