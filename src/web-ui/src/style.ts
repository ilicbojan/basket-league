import styled from 'styled-components';
import { BREAKPOINTS, COLOR } from './app/common/util/variables';

const Stats = styled.div`
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
  justify-content: space-between;
  padding: 10px;
  font-size: 1.8rem;

  @media ${BREAKPOINTS.lg} {
    grid-template-columns: repeat(4, 1fr);
  }

  & div {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: calc((100vw - 40px) / 3);
    border: 2px solid ${COLOR.white};
    border-radius: 50%;

    @media ${BREAKPOINTS.lg} {
      height: calc((1200px - 40px) / 4);
    }
  }

  & h6 {
    font-weight: normal;
    font-size: 1.4rem;
  }
`;

export const Style = {
  Stats,
};
