import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import MatchesListItem from '../list-item/MatchesListItem';

const H2HMatches = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { h2h, loadingH2H } = rootStore.matchStore;

  if (loadingH2H) return <LoadingSpinner />;

  return (
    <div>
      {h2h.map((match) => (
        <MatchesListItem match={match} key={match.id} />
      ))}
    </div>
  );
});

export default H2HMatches;
