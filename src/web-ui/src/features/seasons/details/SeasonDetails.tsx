import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import TabNav from '../../../app/common/tabs/tab-nav/TabNav';
import Tab from '../../../app/common/tabs/tab/Tab';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import SeasonMatches from '../matches/SeasonMatches';
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
      season,
      loading,
    } = rootStore.seasonStore;
    const { setSeasonId } = rootStore.matchStore;

    const [selected, setSelected] = useState<string>('Standings');
    const tabs = ['Standings', 'Matches', 'Results', 'Leaders', 'Archive'];

    useEffect(() => {
      const id = Number.parseInt(match.params.id);
      setSeasonId(id);
      loadSeason(id);
      loadStandings(id);
    }, [loadStandings, loadSeason, match.params.id, history, setSeasonId]);

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
            <SeasonMatches isPlayed={false} />
          </Tab>
          <Tab isSelected={selected === 'Results'}>
            <SeasonMatches isPlayed={true} />
          </Tab>
          <Tab isSelected={selected === 'Leaders'}>Leaders</Tab>
          <Tab isSelected={selected === 'Archive'}>Archive</Tab>
        </TabNav>
      </div>
    );
  }
);

export default SeasonDetails;
