import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import TabNav from '../../../app/common/tabs/tab-nav/TabNav';
import Tab from '../../../app/common/tabs/tab/Tab';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import SeasonStandings from '../../seasons/standings/SeasonStandings';
import H2HMatches from '../h2h/H2HMatches';
import MatchLineup from '../lineup/MatchLineup';
import MatchStats from '../stats/MatchStats';

interface IProps {
  id: string;
}

const MatchDetails: React.FC<RouteComponentProps<IProps>> = observer(
  ({ match: routeMatch, history }) => {
    const rootStore = useContext(RootStoreContext);
    const {
      loadMatch,
      loadMatchStats,
      loadH2HMatches,
      loadingMatches,
      match,
    } = rootStore.matchStore;
    const { loadLineup } = rootStore.matchPlayerStore;
    const { loadStandings } = rootStore.seasonStore;

    useEffect(() => {
      const id = Number.parseInt(routeMatch.params.id);
      loadMatch(id).then((seasonId) => loadStandings(seasonId!));
      loadLineup(id);
      loadMatchStats(id);
      loadH2HMatches(id);
    }, [
      loadMatch,
      routeMatch.params.id,
      history,
      loadLineup,
      loadStandings,
      loadMatchStats,
      loadH2HMatches,
    ]);

    const [selected, setSelected] = useState<string>('Summary');
    const tabs = ['Summary', 'Player Stats', 'H2H', 'Standings'];

    if (loadingMatches) return <LoadingSpinner />;

    return (
      <div>
        <div>
          <div>Round {match?.round}</div>
          <div>
            {match?.date} - {match?.time}
          </div>
          <div>{match?.homeTeam.name}</div>
          <div>
            {match?.homePoints} : {match?.awayPoints}
          </div>
          <div>{match?.awayTeam.name}</div>
        </div>
        <TabNav tabs={tabs} selected={selected} setSelected={setSelected}>
          <Tab isSelected={selected === 'Summary'}>
            <MatchStats />
          </Tab>
          <Tab isSelected={selected === 'Player Stats'}>
            <MatchLineup />
          </Tab>
          <Tab isSelected={selected === 'H2H'}>
            <H2HMatches />
          </Tab>
          <Tab isSelected={selected === 'Standings'}>
            <SeasonStandings />
          </Tab>
        </TabNav>
      </div>
    );
  }
);

export default MatchDetails;
