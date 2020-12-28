import { observer } from 'mobx-react-lite';
import React, { Fragment, useContext, useEffect } from 'react';
import { RootStoreContext } from '../../../app/stores/rootStore';
import MatchesList from '../../matches/list/MatchesList';

interface IProps {
  isPlayed: boolean;
}

const SeasonMatches: React.FC<IProps> = observer(({ isPlayed }) => {
  const rootStore = useContext(RootStoreContext);
  const { setMatchesPredicate } = rootStore.matchStore;

  const isPlayedString = isPlayed ? 'true' : 'false';

  useEffect(() => {
    setMatchesPredicate('isPlayed', isPlayedString);
  }, [setMatchesPredicate]);

  return (
    <Fragment>
      <MatchesList />
    </Fragment>
  );
});

export default SeasonMatches;
