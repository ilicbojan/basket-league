import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import MatchItem from '../match-item/MatchItem';
import ResultItem from '../result-item/ResultItem';

interface IProps {
  seasonId?: number;
  teamId?: number;
  isPlayed: boolean;
}

const MatchesList: React.FC<IProps> = observer(
  ({ seasonId, teamId, isPlayed }) => {
    const rootStore = useContext(RootStoreContext);
    const {
      setMatchPredicates,
      matches,
      loadingMatches,
    } = rootStore.matchStore;

    const isPlayedString = isPlayed ? 'true' : 'false';

    useEffect(() => {
      if (seasonId) {
        const values = {
          isPlayed: isPlayedString,
          seasonId: seasonId,
        };
        setMatchPredicates(values);
      } else if (teamId) {
        const values = {
          isPlayed: isPlayedString,
          teamId: teamId,
        };
        setMatchPredicates(values);
      }
    }, [setMatchPredicates, seasonId, teamId, isPlayedString]);

    if (loadingMatches) return <LoadingSpinner />;

    return (
      <div>
        {matches.map((match) =>
          isPlayed ? (
            <ResultItem match={match} key={match.id} />
          ) : (
            <MatchItem match={match} key={match.id} />
          )
        )}
      </div>
    );
  }
);

export default MatchesList;
