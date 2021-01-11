import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import TabNav from '../../../app/common/tabs/tab-nav/TabNav';
import Tab from '../../../app/common/tabs/tab/Tab';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import PlayerAllTimeStats from '../all-time-stats/PlayerAllTimeStats';
import PlayerCurrentStats from '../current-stats/PlayerCurrentStats';

interface IProps {
  id: string;
}

const PlayerDetails: React.FC<RouteComponentProps<IProps>> = observer(
  ({ match, history }) => {
    const rootStore = useContext(RootStoreContext);
    const {
      loadPlayer,
      loadPlayerCurrentStats,
      loadPlayerAllTimeStats,
      player,
      loadingPlayers,
    } = rootStore.playerStore;

    const [selected, setSelected] = useState<string>('Current stats');

    const tabs = ['Current stats', 'All time stats'];
    const id = Number.parseInt(match.params.id);

    useEffect(() => {
      loadPlayer(id);
      loadPlayerCurrentStats(id);
      loadPlayerAllTimeStats(id);
    }, [
      loadPlayer,
      loadPlayerCurrentStats,
      loadPlayerAllTimeStats,
      id,
      history,
      match.params.id,
    ]);

    if (loadingPlayers) return <LoadingSpinner />;

    return (
      <div>
        <div>
          {player?.firstName} + {player?.lastName}
        </div>
        <div># {player?.jerseyNumber}</div>
        <TabNav tabs={tabs} selected={selected} setSelected={setSelected}>
          <Tab isSelected={selected === 'Current stats'}>
            <PlayerCurrentStats />
          </Tab>
          <Tab isSelected={selected === 'All time stats'}>
            <PlayerAllTimeStats />
          </Tab>
        </TabNav>
      </div>
    );
  }
);

export default PlayerDetails;
