import styled from 'styled-components';
import { BREAKPOINTS } from '../../../app/common/util/variables';

const ContactForm = styled.div`
  padding: 20px;

  @media ${BREAKPOINTS.lg} {
    width: 60%;
    margin: 0 auto;
  }

  & button {
    margin-top: 20px;
  }
`;

export const S = {
  ContactForm,
};
