import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const PlayersList = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 10px;
  padding: 10px;

  @media ${BREAKPOINTS.lg} {
    grid-template-columns: repeat(4, 1fr);
    padding: 20px 0;
    gap: 20px;
  }
`;

export const S = {
  PlayersList,
};
