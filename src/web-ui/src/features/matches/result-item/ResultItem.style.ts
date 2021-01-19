import styled from 'styled-components';
import { BREAKPOINTS, COLOR } from '../../../app/common/util/variables';

const ResultItem = styled.div`
  display: grid;
  grid-template-columns: 15% 35% 35% 15%;
  align-items: center;
  padding: 5px 10px;
  font-size: 1.4rem;
  background-color: ${COLOR.gray2};
  border-bottom: 1px ${COLOR.gray1} solid;

  &:hover {
    background-color: ${COLOR.gray3};
  }

  @media ${BREAKPOINTS.lg} {
    font-size: 1.6rem;
    padding: 8px 15px;
  }

  & .date {
    font-size: 1.2rem;

    @media ${BREAKPOINTS.lg} {
      font-size: 1.4rem;
    }
  }

  & .score {
    text-align: right;
  }
`;

export const S = {
  ResultItem,
};
