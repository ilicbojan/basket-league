import styled from 'styled-components';
import { BREAKPOINTS, COLOR } from '../../../app/common/util/variables';

const HomeCounters = styled.div`
  display: grid;
  margin: 20px 10px;

  @media ${BREAKPOINTS.lg} {
    margin: 40px 0;
    grid-template-columns: repeat(4, 1fr);
    column-gap: 30px;
  }

  & .counter {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 160px;
    margin: 10px 0;
    border: 2px solid ${COLOR.primary};

    @media ${BREAKPOINTS.lg} {
      height: 180px;
      font-size: 2rem;
    }
  }

  & svg {
    margin-bottom: 10px;
    fill: ${COLOR.primary};
    height: 60px;
    width: 60px;

    @media ${BREAKPOINTS.lg} {
      height: 70px;
      width: 70px;
    }
  }
`;

export const S = {
  HomeCounters,
};
