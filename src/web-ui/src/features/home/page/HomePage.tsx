import React from 'react';
import { Link } from 'react-router-dom';
import HomeCounters from '../counters/HomeCounters';
import HomeHeader from '../header/HomeHeader';
import Social from '../social/HomeSocial';

const HomePage = () => {
  return (
    <div>
      <HomeHeader />
      <HomeCounters />
      <Social />
      <div>Home</div>
      <Link to='/seasons/2'>Season</Link>
    </div>
  );
};

export default HomePage;
