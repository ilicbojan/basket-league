import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import MatchInfo from '../../../features/matches/info/MatchInfo';
import MatchLineup from '../../../features/matches/lineup/MatchLineup';
import { S } from './MatchManage.style';

interface IParam {
  id: string;
}

const MatchManage = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { loadMatch, loadingMatches, match } = rootStore.matchStore;
  const { loadLineup, lineup } = rootStore.matchPlayerStore;

  const { id } = useParams<IParam>();
  const matchId = Number.parseInt(id);

  useEffect(() => {
    loadMatch(matchId);
    loadLineup(matchId);
  }, [loadMatch, loadLineup, matchId]);

  const homeHaveLineup = lineup.find((x) => x.team.id === match?.homeTeam.id)
    ? true
    : false;
  const awayHaveLineup = lineup.find((x) => x.team.id === match?.awayTeam.id)
    ? true
    : false;

  if (loadingMatches || !match) return <LoadingSpinner />;

  return (
    <S.MatchManage>
      <MatchInfo />
      <div>{match.homeTeam.name}</div>

      {!homeHaveLineup && (
        <Link to={`/matches/${match.id}/teams/${match.homeTeam.id}/lineup`}>
          Add Lineup
        </Link>
      )}
      <div>{match.awayTeam.name}</div>
      {!awayHaveLineup && (
        <Link to={`/matches/${match.id}/teams/${match.awayTeam.id}/lineup`}>
          Add Lineup
        </Link>
      )}

      <MatchLineup />
    </S.MatchManage>
  );
});

export default MatchManage;
