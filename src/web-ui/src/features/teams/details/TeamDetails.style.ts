import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const TeamDetails = styled.div``;

const Team = styled.div`
  padding: 10px;
  display: flex;
  justify-content: left;
  align-items: center;

  @media ${BREAKPOINTS.lg} {
    padding: 20px 0;
  }

  & .image {
    margin-right: 10px;
    width: 120px;
  }
`;

export const S = {
  TeamDetails,
  Team,
};
