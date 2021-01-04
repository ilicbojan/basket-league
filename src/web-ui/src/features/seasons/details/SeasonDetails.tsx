import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import TabNav from '../../../app/common/tabs/tab-nav/TabNav';
import Tab from '../../../app/common/tabs/tab/Tab';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import MatchesList from '../../matches/list/MatchesList';
import SeasonPlayersStats from '../players-stats/SeasonPlayersStats';
import SeasonStandings from '../standings/SeasonStandings';

interface IProps {
  id: string;
}

const SeasonDetails: React.FC<RouteComponentProps<IProps>> = observer(
  ({ match, history }) => {
    const rootStore = useContext(RootStoreContext);
    const {
      loadStandings,
      loadSeason,
      loadPlayersStats,
      season,
      loading,
    } = rootStore.seasonStore;

    const [selected, setSelected] = useState<string>('Standings');
    const tabs = ['Standings', 'Matches', 'Results', 'Leaders', 'Archive'];
    const id = Number.parseInt(match.params.id);

    useEffect(() => {
      loadSeason(id);
      loadStandings(id);
      loadPlayersStats(id);
    }, [
      loadStandings,
      loadSeason,
      match.params.id,
      history,
      loadPlayersStats,
      id,
    ]);

    if (loading) return <LoadingSpinner />;

    return (
      <div>
        <div>
          {season?.name} - {season?.year}
        </div>
        <TabNav tabs={tabs} selected={selected} setSelected={setSelected}>
          <Tab isSelected={selected === 'Standings'}>
            <SeasonStandings />
          </Tab>
          <Tab isSelected={selected === 'Matches'}>
            <MatchesList isPlayed={false} seasonId={id} />
          </Tab>
          <Tab isSelected={selected === 'Results'}>
            <MatchesList isPlayed={true} seasonId={id} />
          </Tab>
          <Tab isSelected={selected === 'Leaders'}>
            <SeasonPlayersStats />
          </Tab>
          <Tab isSelected={selected === 'Archive'}>Archive</Tab>
        </TabNav>
      </div>
    );
  }
);

export default SeasonDetails;
