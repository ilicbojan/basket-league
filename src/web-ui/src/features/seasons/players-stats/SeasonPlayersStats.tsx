import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import Table from '../../../app/common/table/Table';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './SeasonPlayersStats.style';

const SeasonPlayersStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { loadingPlayersStats, playersStats } = rootStore.seasonStore;

  return (
    <S.SeasonPlayersStats>
      <Table>
        <thead>
          <tr>
            <th>#</th>
            <th>Player</th>
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
                  <Link to={`/players/${player.id}`} className='player'>
                    <img src='/images/user.jpg' alt='' className='image' />
                    <div className='name'>
                      {player.lastName} {player.firstName}
                    </div>
                  </Link>
                </td>
                <td>{player.matchesPlayed}</td>
                <td>{player.points}</td>
                <td>{player.pointsAvg}</td>
                <td>{player.assists}</td>
                <td>{player.assistsAvg}</td>
                <td>{player.fouls}</td>
                <td>{player.foulsAvg}</td>
              </tr>
            ))
          )}
        </tbody>
      </Table>
    </S.SeasonPlayersStats>
  );
});

export default SeasonPlayersStats;
