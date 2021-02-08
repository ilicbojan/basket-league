import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const HomeHeader = styled.div`
  background-image: linear-gradient(rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7)),
    url('/images/landing.jpg');
  background-size: cover;
  background-attachment: fixed;
  background-position: center;
  min-height: 94vh;
  position: relative;
  z-index: 0;
  display: flex;
  justify-content: center;
  align-items: center;

  & .heading {
    margin: 0 10px;
  }

  & h1 {
    text-transform: uppercase;
    font-size: 2.5rem;
    margin-bottom: 10px;

    @media ${BREAKPOINTS.lg} {
      font-size: 4rem;
    }
  }

  & h2 {
    font-size: 1.8rem;
    margin-bottom: 20px;

    @media ${BREAKPOINTS.lg} {
      font-size: 2.5rem;
    }
  }
`;

export const S = {
  HomeHeader,
};
