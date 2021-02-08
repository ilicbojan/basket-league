import React from 'react';
import { S } from './HomeCounters.style';

const HomeCounters = () => {
  return (
    <S.HomeCounters>
      <div className='counter'>
        <h3>2</h3>
        <div>Leagues</div>
      </div>
      <div className='counter'>
        <h3>15</h3>
        <div>Teams</div>
      </div>
      <div className='counter'>
        <h3>57</h3>
        <div>Players</div>
      </div>
      <div className='counter'>
        <h3>3</h3>
        <div>Fields</div>
      </div>
    </S.HomeCounters>
  );
};

export default HomeCounters;
