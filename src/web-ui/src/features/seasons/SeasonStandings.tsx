import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import Table from '../../app/common/table/Table';
import LoadingSpinner from '../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../app/stores/rootStore';

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

    // if (loading)
    //   return (
    //     <Table>
    //       <thead>
    //         <tr>
    //           <th>Loading...</th>
    //         </tr>
    //       </thead>
    //       <tbody>
    //         <tr>
    //           <td className='spin'>
    //             <LoadingSpinner />
    //           </td>
    //         </tr>
    //       </tbody>
    //     </Table>
    //   );

    return (
      <div>
        <Table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>OU</th>
              <th>PO</th>
              <th>IZ</th>
              <th>P+</th>
              <th>P-</th>
              <th>P+-</th>
              <th>BO</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr>
                <td className='spin'>
                  <LoadingSpinner />
                </td>
              </tr>
            ) : (
              standings?.teams.map((team) => (
                <tr key={team.id}>
                  <td>{team.id}</td>
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
