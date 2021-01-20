import styled from 'styled-components';
import { BREAKPOINTS, COLOR } from '../../../app/common/util/variables';

const MatchDetails = styled.div``;

const Info = styled.div`
  & .league {
    padding: 10px 10px 5px 10px;
    font-size: 1rem;
    text-transform: uppercase;
    border-bottom: 1px solid ${COLOR.gray3};

    @media ${BREAKPOINTS.lg} {
      padding: 10px 0 5px 0;
      font-size: 1.4rem;
    }
  }

  & .match {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px;

    @media ${BREAKPOINTS.lg} {
      justify-content: space-around;
      padding: 20px 10px;
    }
  }

  & .team {
    display: flex;
    flex-direction: column;
    align-items: center;
    font-size: 1.4rem;

    @media ${BREAKPOINTS.lg} {
      font-size: 1.8rem;
    }
  }

  & .image {
    width: 60px;

    @media ${BREAKPOINTS.lg} {
      width: 120px;
    }
  }

  & .info {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  & .date {
    font-size: 1.2rem;

    @media ${BREAKPOINTS.lg} {
      font-size: 1.4rem;
    }
  }

  & .score {
    font-size: 2.5rem;

    @media ${BREAKPOINTS.lg} {
      font-size: 4rem;
    }
  }
`;

export const S = {
  MatchDetails,
  Info,
};
