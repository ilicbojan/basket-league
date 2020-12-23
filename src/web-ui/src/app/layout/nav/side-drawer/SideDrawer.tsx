import React, { useContext } from 'react';
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
          <NavLink to='/'>Početna</NavLink>
        </li>
      </ul>
    </S.SideDrawer>
  );
});

export default SideDrawer;
