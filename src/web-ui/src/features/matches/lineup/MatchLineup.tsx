import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import Table from '../../../app/common/table/Table';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './MatchLineup.style';

const MatchLineup = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { loadingLineup, lineup } = rootStore.matchPlayerStore;

  return (
    <S.MatchLineup>
      <Table>
        <thead>
          <tr>
            <th>#</th>
            <th>Player</th>
            <th>Team</th>
            <th>PTS</th>
            <th>AST</th>
            <th>PF</th>
          </tr>
        </thead>
        <tbody>
          {loadingLineup ? (
            <tr>
              <td className='spin' colSpan={9}>
                <LoadingSpinner />
              </td>
            </tr>
          ) : (
            lineup.map((player) => (
              <tr key={player.id}>
                <td>{player.id}</td>
                <td>
                  <Link to={`/players/${player.id}`} className='player'>
                    <img src='/images/user.jpg' alt='' className='image' />
                    <div className='name'>
                      {player.lastName}{' '}
                      {player.firstName.slice(0, 1).toUpperCase()}.
                    </div>
                  </Link>
                </td>
                <td>{player.team.name.slice(0, 3).toUpperCase()}</td>
                <td>{player.points}</td>
                <td>{player.assists}</td>
                <td>{player.fouls}</td>
              </tr>
            ))
          )}
        </tbody>
      </Table>
    </S.MatchLineup>
  );
});

export default MatchLineup;
