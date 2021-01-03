import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import TabNav from '../../../app/common/tabs/tab-nav/TabNav';
import Tab from '../../../app/common/tabs/tab/Tab';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';

interface IProps {
  id: string;
}

const TeamDetails: React.FC<RouteComponentProps<IProps>> = observer(
  ({ match, history }) => {
    const rootStore = useContext(RootStoreContext);
    const { loadTeam, team, loading } = rootStore.teamStore;

    const [selected, setSelected] = useState<string>('Stats');
    const tabs = ['Stats', 'Players', 'Matches', 'Results', 'Standings'];

    useEffect(() => {
      const id = Number.parseInt(match.params.id);
      loadTeam(id);
    }, [loadTeam, match.params.id, history]);

    if (loading) return <LoadingSpinner />;

    return (
      <div>
        <div>{team?.name}</div>
        <TabNav tabs={tabs} selected={selected} setSelected={setSelected}>
          <Tab isSelected={selected === 'Stats'}>Stats</Tab>
          <Tab isSelected={selected === 'Players'}>Players</Tab>
          <Tab isSelected={selected === 'Matches'}>Matches</Tab>
          <Tab isSelected={selected === 'Results'}>Results</Tab>
          <Tab isSelected={selected === 'Standings'}>Standings</Tab>
        </TabNav>
      </div>
    );
  }
);

export default TeamDetails;
