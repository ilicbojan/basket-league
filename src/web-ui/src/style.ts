import styled from 'styled-components';
import { COLOR } from './app/common/util/variables';

const Stats = styled.div`
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
  justify-content: space-between;
  padding: 10px;
  font-size: 1.8rem;

  & div {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: calc((100vw - 40px) / 3);
    border: 2px solid ${COLOR.white};
    border-radius: 50%;
  }

  & h6 {
    font-weight: normal;
    font-size: 1.4rem;
  }
`;

export const Style = {
  Stats,
};
