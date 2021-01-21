import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './MatchStats.style';

const MatchStats = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { loadingMatchStats, matchStats } = rootStore.matchStore;

  if (loadingMatchStats) return <LoadingSpinner />;

  return (
    <S.MatchStats>
      <div className='stats'>
        <div>{matchStats?.homePoints}</div>
        <div>Points</div>
        <div>{matchStats?.awayPoints}</div>
      </div>
      <S.Bar home={matchStats?.homePoints!} away={matchStats?.awayPoints!}>
        <div className='home'></div>
        <div className='away'></div>
      </S.Bar>
      <div className='stats'>
        <div>{matchStats?.homeAssists}</div>
        <div>Assists</div>
        <div>{matchStats?.awayAssists}</div>
      </div>
      <S.Bar home={matchStats?.homeAssists!} away={matchStats?.awayAssists!}>
        <div className='home'></div>
        <div className='away'></div>
      </S.Bar>
      <div className='stats'>
        <div>{matchStats?.homeFouls}</div>
        <div>Fouls</div>
        <div>{matchStats?.awayFouls}</div>
      </div>
      <S.Bar home={matchStats?.homeFouls!} away={matchStats?.awayFouls!}>
        <div className='home'></div>
        <div className='away'></div>
      </S.Bar>
    </S.MatchStats>
  );
});

export default MatchStats;
