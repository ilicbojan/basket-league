import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const PlayerDetails = styled.div``;

const Info = styled.div`
  padding: 10px;
  display: flex;
  align-items: center;

  @media ${BREAKPOINTS.lg} {
    padding: 20px 0;
  }

  & .image {
    margin-right: 10px;
    width: 100px;

    @media ${BREAKPOINTS.lg} {
      width: 120px;
    }
  }
`;

export const S = {
  PlayerDetails,
  Info,
};
