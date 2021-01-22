import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const TeamStats = styled.div`
  margin-top: 10px;

  @media ${BREAKPOINTS.lg} {
    margin-top: 20px;
  }
`;

export const S = {
  TeamStats,
};
