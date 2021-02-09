import React from 'react';
import { FaPeopleCarry, FaUserAlt } from 'react-icons/fa';
import { GiBasketballBasket, GiPoliceBadge } from 'react-icons/gi';
import { S } from './HomeCounters.style';

const HomeCounters = () => {
  return (
    <S.HomeCounters>
      <div className='counter'>
        <GiPoliceBadge />
        <h3>2</h3>
        <div>Leagues</div>
      </div>
      <div className='counter'>
        <FaPeopleCarry />
        <h3>15</h3>
        <div>Teams</div>
      </div>
      <div className='counter'>
        <FaUserAlt />
        <h3>57</h3>
        <div>Players</div>
      </div>
      <div className='counter'>
        <GiBasketballBasket />
        <h3>3</h3>
        <div>Fields</div>
      </div>
    </S.HomeCounters>
  );
};

export default HomeCounters;
