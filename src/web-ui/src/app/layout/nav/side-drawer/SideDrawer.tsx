import React from 'react';
import { NavLink } from 'react-router-dom';
import { S } from './SideDrawer.style';
import { observer } from 'mobx-react-lite';
import { FaHome } from 'react-icons/fa';

interface IProps {
  show: boolean;
  click: () => void;
}

const SideDrawer: React.FC<IProps> = observer(({ show, click }) => {
  return (
    <S.SideDrawer show={show}>
      <ul>
        <li onClick={click}>
          <FaHome />
          <NavLink to='/'>Home</NavLink>
        </li>
        <hr />
        <S.SubNavLinks>
          <li onClick={click}>
            <NavLink to='/seasons/2'>1. Liga</NavLink>
          </li>
          <li onClick={click}>
            <NavLink to='/seasons/1'>2. Liga</NavLink>
          </li>
        </S.SubNavLinks>
      </ul>
    </S.SideDrawer>
  );
});

export default SideDrawer;
