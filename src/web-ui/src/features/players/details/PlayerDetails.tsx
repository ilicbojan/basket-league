import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';

interface IProps {
  id: string;
}

const PlayerDetails: React.FC<RouteComponentProps<IProps>> = observer(
  ({ match, history }) => {
    const rootStore = useContext(RootStoreContext);
    const { loadPlayer, player, loadingPlayers } = rootStore.playerStore;

    const id = Number.parseInt(match.params.id);

    useEffect(() => {
      loadPlayer(id);
    }, [loadPlayer, id, history, match.params.id]);

    if (loadingPlayers) return <LoadingSpinner />;

    return (
      <div>
        <div>
          {player?.firstName} + {player?.lastName}
        </div>
        <div># {player?.jerseyNumber}</div>
      </div>
    );
  }
);

export default PlayerDetails;
