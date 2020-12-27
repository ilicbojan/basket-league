import React from 'react';

interface IProps {
  isSelected: boolean;
}

const Tab: React.FC<IProps> = ({ isSelected, children }) => {
  return <div>{isSelected ? children : null}</div>;
};

export default Tab;
