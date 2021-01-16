import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import TabNav from '../../../app/common/tabs/tab-nav/TabNav';
import Tab from '../../../app/common/tabs/tab/Tab';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import MatchesList from '../../matches/list/MatchesList';
import PlayersList from '../../players/list/PlayersList';
import SeasonStandings from '../../seasons/standings/SeasonStandings';
import TeamStats from '../stats/TeamStats';
import { S } from './TeamDetails.style';

interface IProps {
  id: string;
}

const TeamDetails: React.FC<RouteComponentProps<IProps>> = observer(
  ({ match, history }) => {
    const rootStore = useContext(RootStoreContext);
    const {
      loadTeam,
      loadTeamCurrentStats,
      loadTeamAllTimeStats,
      team,
      loading,
    } = rootStore.teamStore;
    const { loadStandings } = rootStore.seasonStore;

    const [selected, setSelected] = useState<string>('Stats');
    const tabs = ['Stats', 'Players', 'Matches', 'Results', 'Standings'];
    const id = Number.parseInt(match.params.id);

    useEffect(() => {
      loadTeam(id).then((team) => loadStandings(team.currentSeasonId));
      loadTeamCurrentStats(id);
      loadTeamAllTimeStats(id);
    }, [
      loadTeam,
      match.params.id,
      history,
      id,
      loadStandings,
      loadTeamCurrentStats,
      loadTeamAllTimeStats,
    ]);

    if (loading) return <LoadingSpinner />;

    return (
      <S.TeamDetails>
        <S.Team>
          <div className='img'>slika</div>
          <div>{team?.name}</div>
        </S.Team>
        <TabNav tabs={tabs} selected={selected} setSelected={setSelected}>
          <Tab isSelected={selected === 'Stats'}>
            <TeamStats />
          </Tab>
          <Tab isSelected={selected === 'Players'}>
            <PlayersList teamId={id} />
          </Tab>
          <Tab isSelected={selected === 'Matches'}>
            <MatchesList isPlayed={false} teamId={id} />
          </Tab>
          <Tab isSelected={selected === 'Results'}>
            <MatchesList isPlayed={true} teamId={id} />
          </Tab>
          <Tab isSelected={selected === 'Standings'}>
            <SeasonStandings />
          </Tab>
        </TabNav>
      </S.TeamDetails>
    );
  }
);

export default TeamDetails;
