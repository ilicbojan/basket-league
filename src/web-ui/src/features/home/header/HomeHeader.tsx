import React from 'react';
import Button from '../../../app/common/button/Button';
import { S } from './HomeHeader.style';

const HomeHeader = () => {
  return (
    <S.HomeHeader className='fullWidth'>
      <div className='heading'>
        <h1>Welcome to amateur 3x3 basket league</h1>
        <h2>Create team, call friends and join our league</h2>
        <Button color='primary'>Create Team</Button>
      </div>
    </S.HomeHeader>
  );
};

export default HomeHeader;
