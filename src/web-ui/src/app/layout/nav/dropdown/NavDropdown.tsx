import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { S } from './NavDropdown.style';

const NavDropdown = () => {
  const [click, setClick] = useState(false);

  const handleClick = () => setClick(!click);

  return (
    <S.NavDropdown
      onClick={handleClick}
      className={click ? 'dropdown-menu clicked' : 'dropdown-menu'}
    >
      <li onClick={() => setClick(false)}>
        <NavLink to='/seasons/2'>1. Liga</NavLink>
      </li>
      <li onClick={() => setClick(false)}>
        <NavLink to='/seasons/1'>2.Liga</NavLink>
      </li>
    </S.NavDropdown>
  );
};

export default NavDropdown;
