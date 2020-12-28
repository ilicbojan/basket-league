import React from 'react';

interface IProps {
  isSelected: boolean;
  onClick?: () => void;
}

const Tab: React.FC<IProps> = ({ isSelected, children, onClick }) => {
  return <div onClick={onClick}>{isSelected ? children : null}</div>;
};

export default Tab;
