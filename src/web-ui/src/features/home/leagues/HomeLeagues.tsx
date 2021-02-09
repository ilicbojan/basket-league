import React from 'react';
import { Link } from 'react-router-dom';
import { S } from './HomeLeagues.style';

const HomeLeagues = () => {
  return (
    <S.HomeLeagues>
      <div className='league'>
        <Link to='/seasons/2'>
          <img src='/images/league.jpg' alt='' className='image' />
          <div className='name'>1. League</div>
        </Link>
      </div>
      <div className='league'>
        <Link to='/seasons/1'>
          <img src='/images/league.jpg' alt='' className='image' />
          <div className='name'>2. League</div>
        </Link>
      </div>
    </S.HomeLeagues>
  );
};

export default HomeLeagues;
