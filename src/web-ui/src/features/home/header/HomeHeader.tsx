import React from 'react';
import { Link } from 'react-router-dom';
import Button from '../../../app/common/button/Button';
import { S } from './HomeHeader.style';

const HomeHeader = () => {
  return (
    <S.HomeHeader className='fullWidth'>
      <div className='heading'>
        <h1>Welcome to amateur 3x3 basket league</h1>
        <h2>Create team, call friends and join our league</h2>
        <Link to='/contact'>
          <Button color='primary'>Create Team</Button>
        </Link>
      </div>
    </S.HomeHeader>
  );
};

export default HomeHeader;
