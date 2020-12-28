import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import MatchesListItem from '../list-item/MatchesListItem';

const MatchesList = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { matches, loadingMatches } = rootStore.matchStore;

  if (loadingMatches) return <LoadingSpinner />;

  return (
    <div>
      <div>Results</div>
      {matches.map((match) => (
        <MatchesListItem match={match} key={match.id} />
      ))}
    </div>
  );
});

export default MatchesList;
