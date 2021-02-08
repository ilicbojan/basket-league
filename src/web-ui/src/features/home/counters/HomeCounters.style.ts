import styled from 'styled-components';
import { BREAKPOINTS, COLOR } from '../../../app/common/util/variables';

const HomeCounters = styled.div`
  display: grid;
  margin: 20px 10px;

  @media ${BREAKPOINTS.lg} {
    grid-template-columns: repeat(4, 1fr);
    column-gap: 40px;
  }

  & .counter {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100px;
    margin: 10px 0;
    border: 2px solid ${COLOR.primary};
  }
`;

export const S = {
  HomeCounters,
};
