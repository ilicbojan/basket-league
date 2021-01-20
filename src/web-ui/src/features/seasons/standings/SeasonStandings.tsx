import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import Table from '../../../app/common/table/Table';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './SeasonStandings.style';

const SeasonStandings = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { standings, loadingStandings } = rootStore.seasonStore;

  return (
    <S.SeasonStandings>
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
                <td>
                  <Link to={`/teams/${team.id}`} className='team'>
                    <img src='/images/team.jpg' alt='' className='image' />
                    <div className='name'>{team.name}</div>
                  </Link>
                </td>
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
    </S.SeasonStandings>
  );
});

export default SeasonStandings;
