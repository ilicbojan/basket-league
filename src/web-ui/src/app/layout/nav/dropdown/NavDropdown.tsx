import React, { useState } from 'react';
import { S } from './NavDropdown.style';

const NavDropdown = () => {
  const [click, setClick] = useState(false);

  const handleClick = () => setClick(!click);

  return (
    <S.NavDropdown
      onClick={handleClick}
      className={click ? 'dropdown-menu clicked' : 'dropdown-menu'}
    ></S.NavDropdown>
  );
};

export default NavDropdown;
