import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import TabNav from '../../../app/common/tabs/tab-nav/TabNav';
import Tab from '../../../app/common/tabs/tab/Tab';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import MatchLineup from '../lineup/MatchLineup';

interface IProps {
  id: string;
}

const MatchDetails: React.FC<RouteComponentProps<IProps>> = observer(
  ({ match: matc, history }) => {
    const rootStore = useContext(RootStoreContext);
    const { loadMatch, loadingMatches, match } = rootStore.matchStore;
    const { loadLineup } = rootStore.matchPlayerStore;

    useEffect(() => {
      const id = Number.parseInt(matc.params.id);
      loadMatch(id);
      loadLineup(id);
    }, [loadMatch, matc.params.id, history, loadLineup]);

    const [selected, setSelected] = useState<string>('Summary');
    const tabs = ['Summary', 'Player Stats', 'H2H', 'Standings'];

    if (loadingMatches) return <LoadingSpinner />;

    return (
      <div>
        <div>
          <div>{match?.round}</div>
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
          <Tab isSelected={selected === 'Summary'}>Statistics</Tab>
          <Tab isSelected={selected === 'Player Stats'}>
            <MatchLineup />
          </Tab>
          <Tab isSelected={selected === 'H2H'}>H2H</Tab>
          <Tab isSelected={selected === 'Standings'}>Standings</Tab>
        </TabNav>
      </div>
    );
  }
);

export default MatchDetails;
