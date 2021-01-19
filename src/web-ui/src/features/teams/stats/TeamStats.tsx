import React, { useState } from 'react';
import TabNav from '../../../app/common/tabs/tab-nav/TabNav';
import Tab from '../../../app/common/tabs/tab/Tab';
import TeamAllTimeStats from '../all-time-stats/TeamAllTimeStats';
import TeamCurrentStats from '../current-stats/TeamCurrentStats';
import { S } from './TeamStats.style';

const TeamStats = () => {
  const [selected, setSelected] = useState<string>('Current');
  const tabs = ['Current', 'All time'];

  return (
    <S.TeamStats>
      <TabNav tabs={tabs} selected={selected} setSelected={setSelected}>
        <Tab isSelected={selected === 'Current'}>
          <TeamCurrentStats />
        </Tab>
        <Tab isSelected={selected === 'All time'}>
          <TeamAllTimeStats />
        </Tab>
      </TabNav>
    </S.TeamStats>
  );
};

export default TeamStats;
