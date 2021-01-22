import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const TeamDetails = styled.div``;

const Team = styled.div`
  padding: 10px;
  display: flex;
  justify-content: left;
  align-items: center;
  font-size: 1.4rem;

  @media ${BREAKPOINTS.lg} {
    padding: 20px 0;
  }

  & .image {
    margin-right: 10px;
    width: 100px;

    @media ${BREAKPOINTS.lg} {
      margin-right: 20px;
      width: 120px;
    }
  }

  & .name {
    font-size: 1.6rem;

    @media ${BREAKPOINTS.lg} {
      font-size: 2rem;
    }
  }
`;

export const S = {
  TeamDetails,
  Team,
};
