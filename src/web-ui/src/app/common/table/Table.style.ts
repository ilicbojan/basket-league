import styled from 'styled-components';
import { utilities, COLOR, BREAKPOINTS } from '../util/variables';

const TableSection = styled.div`
  overflow-x: auto;
`;

const Table = styled.table`
  border-collapse: collapse;
  margin: 0;
  font-size: 1.4rem;
  min-width: 300px;
  width: 100%;
  box-shadow: ${utilities.shadow};
  overflow: hidden;

  @media ${BREAKPOINTS.lg} {
    font-size: 1.6rem;
  }

  & thead tr {
    background-color: ${COLOR.gray2};
    color: ${COLOR.white};
    border-color: ${COLOR.gray2};
    text-align: left;
  }

  & th,
  & td {
    padding: 8px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }

  & th {
    font-weight: normal;
  }

  & tbody {
    background-color: ${COLOR.gray1};
  }

  & tbody tr {
    border-bottom: 1px solid ${COLOR.gray3};
  }
  /* 
  & tbody tr:nth-of-type(even) {
    background-color: ${COLOR.gray3};
  } */

  & .loading {
  }

  & .spin {
    position: relative;
    height: 100px;
  }
`;

export const S = {
  Table,
  TableSection,
};
