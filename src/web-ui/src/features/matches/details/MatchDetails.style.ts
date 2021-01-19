import styled from 'styled-components';
import { COLOR } from '../../../app/common/util/variables';

const MatchDetails = styled.div``;

const Info = styled.div`
  & .league {
    padding: 10px 10px 5px 10px;
    font-size: 1rem;
    text-transform: uppercase;
    border-bottom: 1px solid ${COLOR.gray3};
  }

  & .match {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px;
  }

  & .team {
    display: flex;
    flex-direction: column;
    align-items: center;
    font-size: 1.4rem;
  }

  & .image {
    width: 60px;
  }

  & .info {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  & .date {
    font-size: 1.2rem;
  }

  & .score {
    font-size: 2.5rem;
  }
`;

export const S = {
  MatchDetails,
  Info,
};
