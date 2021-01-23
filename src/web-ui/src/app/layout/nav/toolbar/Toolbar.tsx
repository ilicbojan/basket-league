import React, { useState } from 'react';
import { Link, NavLink } from 'react-router-dom';
import { S } from './Toolbar.style';
import Burger from '../burger/Burger';
import { observer } from 'mobx-react-lite';
import { FaAngleDown } from 'react-icons/fa';
import NavDropdown from '../dropdown/NavDropdown';

interface IProps {
  burgerClickHandler: () => void;
}

const Toolbar: React.FC<IProps> = observer(({ burgerClickHandler }) => {
  const [dropdown, setDropdown] = useState(false);

  const onClick = () => {
    setDropdown(!dropdown);
  };

  const onMouseEnter = () => {
    setDropdown(true);
  };

  const onMouseLeave = () => {
    setDropdown(false);
  };

  return (
    <S.Toolbar>
      <S.Navigation>
        <S.Logo>
          <Link to='/'>Logo</Link>
        </S.Logo>
        <S.Spacer></S.Spacer>
        <S.Items>
          <ul>
            <li>
              <NavLink to='/'>Home</NavLink>
            </li>
            <li
              onClick={onClick}
              onMouseEnter={onMouseEnter}
              onMouseLeave={onMouseLeave}
            >
              <button>Leagues</button>
              <FaAngleDown />
              {dropdown && <NavDropdown />}
            </li>
          </ul>
        </S.Items>
        <S.Burger>
          <Burger click={burgerClickHandler} />
        </S.Burger>
      </S.Navigation>
    </S.Toolbar>
  );
});

export default Toolbar;
