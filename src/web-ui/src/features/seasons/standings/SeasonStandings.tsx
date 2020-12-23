import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import Table from '../../../app/common/table/Table';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';

interface IProps {
  id: string;
}

const SeasonStandings: React.FC<RouteComponentProps<IProps>> = observer(
  ({ match, history }) => {
    const rootStore = useContext(RootStoreContext);
    const { loadStandings, standings, loading } = rootStore.seasonStore;

    useEffect(() => {
      loadStandings(Number.parseInt(match.params.id));
    }, [loadStandings, match.params.id, history]);

    return (
      <div>
        <Link to={`/seasons/${standings?.id}/results`}>Results</Link>
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
            {loading ? (
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
  }
);

export default SeasonStandings;
