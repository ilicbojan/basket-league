import styled from 'styled-components';
import { BREAKPOINTS, COLOR } from '../../../app/common/util/variables';

const MatchStats = styled.div`
  padding: 10px;
  font-size: 1.4rem;

  @media ${BREAKPOINTS.lg} {
    font-size: 1.6rem;
  }

  & .stats {
    display: flex;
    justify-content: space-between;
    margin-bottom: 5px;
  }
`;

interface IBarProps {
  home: number;
  away: number;
}

const Bar = styled.div<IBarProps>`
  display: flex;
  margin-bottom: 10px;
  background-color: ${COLOR.gray2};

  @media ${BREAKPOINTS.lg} {
    margin-bottom: 20px;
  }

  & .home,
  & .away {
    height: 14px;

    @media ${BREAKPOINTS.lg} {
      height: 16px;
    }
  }

  & .home {
    width: ${({ home, away }) => {
      if (home === 0 && away > 0) {
        return 0;
      } else if (home > 0 && away === 0) {
        return 100;
      } else {
        return (home / away) * 50;
      }
    }}%;
    background-color: ${COLOR.primary};
  }

  & .away {
    width: ${({ home, away }) => {
      if (away === 0 && home > 0) {
        return 0;
      } else if (away > 0 && home === 0) {
        return 100;
      } else {
        return (away / home) * 50;
      }
    }}%;
    background-color: ${COLOR.secondary};
  }
`;

export const S = {
  MatchStats,
  Bar,
};
