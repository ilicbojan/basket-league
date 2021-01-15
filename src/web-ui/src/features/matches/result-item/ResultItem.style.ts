import styled from 'styled-components';
import { COLOR } from '../../../app/common/util/variables';

const ResultItem = styled.div`
  display: grid;
  grid-template-columns: 15% 35% 35% 15%;
  align-items: center;
  padding: 5px 10px;
  font-size: 1.4rem;
  background-color: ${COLOR.gray2};
  border-bottom: 1px ${COLOR.gray1} solid;

  & .date {
    font-size: 1.2rem;
  }

  & .score {
    text-align: right;
  }
`;

export const S = {
  ResultItem,
};
