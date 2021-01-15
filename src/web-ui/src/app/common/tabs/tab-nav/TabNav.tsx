import React from 'react';
import { S } from './TabNav.style';

interface IProps {
  tabs: string[];
  selected: string;
  setSelected: (tab: string) => void;
}

const TabNav: React.FC<IProps> = ({
  tabs,
  children,
  selected,
  setSelected,
}) => {
  return (
    <div>
      <S.Tabs>
        {tabs.map((tab) => (
          <S.Tab
            onClick={() => setSelected(tab)}
            key={tab}
            active={tab === selected}
          >
            {tab}
          </S.Tab>
        ))}
      </S.Tabs>
      <S.Line></S.Line>
      {children}
    </div>
  );
};

export default TabNav;
