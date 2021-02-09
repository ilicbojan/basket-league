import React from 'react';
import HomeCounters from '../counters/HomeCounters';
import HomeHeader from '../header/HomeHeader';
import HomeLeagues from '../leagues/HomeLeagues';
import Social from '../social/HomeSocial';

const HomePage = () => {
  return (
    <div>
      <HomeHeader />
      <HomeLeagues />
      <HomeCounters />
      <Social />
    </div>
  );
};

export default HomePage;
