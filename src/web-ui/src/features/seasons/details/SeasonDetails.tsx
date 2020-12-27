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
      setMatchesPredicate,
      standings,
      loading,
      setSeasonId,
    } = rootStore.seasonStore;

    const [selected, setSelected] = useState<string>('standings');
    const tabs = ['standings', 'matches', 'results'];

    useEffect(() => {
      setSeasonId(Number.parseInt(match.params.id));
      loadStandings(Number.parseInt(match.params.id));
      setMatchesPredicate('isPlayed', 'true');
    }, [
      loadStandings,
      match.params.id,
      history,
      setMatchesPredicate,
      setSeasonId,
    ]);

    if (loading) return <LoadingSpinner />;

    return (
      <div>
        <div>
          {standings?.name} - {standings?.year}
        </div>
        <TabNav tabs={tabs} selected={selected} setSelected={setSelected}>
          <Tab isSelected={selected === 'standings'}>
            <SeasonStandings />
          </Tab>
          <Tab isSelected={selected === 'matches'}>Text</Tab>
          <Tab isSelected={selected === 'results'}>
            <SeasonMatches />
          </Tab>
        </TabNav>
      </div>
    );
  }
);

export default SeasonDetails;
