import React from 'react';
import { FaFacebookSquare, FaInstagram, FaTwitter } from 'react-icons/fa';
import { S } from './HomeSocial.style';

const Social = () => {
  return (
    <S.Social className='fullWidth'>
      <div className='appContainer social'>
        <a href='www.facebook.com' target='_blank'>
          <FaFacebookSquare size='60px' />
        </a>
        <a href='www.instagram.com' target='_blank'>
          <FaInstagram size='60px' />
        </a>
        <a href='www.twitter.com' target='_blank'>
          <FaTwitter size='60px' />
        </a>
      </div>
    </S.Social>
  );
};

export default Social;
