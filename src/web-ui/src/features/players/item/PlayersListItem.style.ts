import styled from 'styled-components';
import { BREAKPOINTS, COLOR } from '../../../app/common/util/variables';

const PlayersListItem = styled.div`
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

  & .info {
    padding: 0 10px 5px 10px;
  }
`;

export const S = {
  PlayersListItem,
};
