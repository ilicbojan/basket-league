import styled from 'styled-components';
import { COLOR } from '../../../app/common/util/variables';

const Social = styled.div`
  background-color: ${COLOR.gray2};
  color: ${COLOR.primary};
  padding: 18px 0 12px 0;
  margin-bottom: 20px;

  & .social {
    padding: 0 15px;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  & a {
    color: inherit;
  }
`;

export const S = {
  Social,
};
