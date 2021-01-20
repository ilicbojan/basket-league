import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const SeasonPlayersStats = styled.div`
  & .player {
    display: flex;
    justify-content: left;
    align-items: center;
  }

  & .name:hover {
    text-decoration: underline;
  }

  & .image {
    width: 23px;
    margin-right: 8px;

    @media ${BREAKPOINTS.lg} {
      width: 27px;
    }
  }
`;

export const S = {
  SeasonPlayersStats,
};
