import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { RootStoreContext } from '../../../app/stores/rootStore';

const SeasonMatches = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { matches } = rootStore.seasonStore;

  return (
    <div>
      <div>Results</div>
      {matches.map((match) => (
        <div key={match.id}>
          <div>{match.round}</div>
          <div>{match.date}</div>
          <div>{match.time}</div>
          <div>{match.homeTeam.name}</div>
          <div>{match.homePoints}</div>
          <div>{match.awayPoints}</div>
          <div>{match.awayTeam.name}</div>
        </div>
      ))}
    </div>
  );
});

export default SeasonMatches;
