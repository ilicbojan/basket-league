import styled from 'styled-components';
import { BREAKPOINTS, COLOR } from '../../../app/common/util/variables';

const HomeLeagues = styled.div`
  margin: 20px 10px;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 20px;

  @media ${BREAKPOINTS.lg} {
    margin: 40px 0;
    grid-template-columns: repeat(4, 1fr);
    gap: 30px;
  }

  & .league {
    background-color: ${COLOR.gray2};
    font-size: 1.4rem;
    transition: transform 0.3s;

    &:hover {
      transform: scale(1.03);
    }

    @media ${BREAKPOINTS.lg} {
      font-size: 1.6rem;
    }

    & .image {
      width: 100%;
    }

    & .name {
      padding: 0 10px 5px 10px;
    }
  }
`;

export const S = {
  HomeLeagues,
};
