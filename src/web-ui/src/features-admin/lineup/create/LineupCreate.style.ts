import styled from 'styled-components';

const LineupCreate = styled.div`
  padding: 10px;

  & .checkboxes {
    display: flex;
    flex-direction: column;
  }

  & .checkbox {
    margin-top: 10px;
  }

  & input[type='checkbox'] {
    margin-right: 5px;
  }
`;

export const S = {
  LineupCreate,
};
