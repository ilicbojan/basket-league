import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const SeasonDetails = styled.div``;

const Info = styled.div`
  padding: 10px;
  display: flex;
  align-items: center;

  @media ${BREAKPOINTS.lg} {
    padding: 20px 0;
  }

  & .image {
    width: 120px;
    margin-right: 10px;
  }
`;

export const S = {
  SeasonDetails,
  Info,
};
