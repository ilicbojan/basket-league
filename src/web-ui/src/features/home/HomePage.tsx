import React from 'react';
import { Link } from 'react-router-dom';

const HomePage = () => {
  return (
    <div>
      <div>Home</div>
      <Link to='/seasons/2'>Season</Link>
    </div>
  );
};

export default HomePage;
