import styled from 'styled-components';
import { BREAKPOINTS, COLOR, utilities } from '../../util/variables';

interface ITabProps {
  active: boolean;
}

const Tabs = styled.ul`
  display: inline-flex;
  margin-top: 10px;
  color: ${COLOR.white};
`;

export const Tab = styled.li`
  font-size: 1.2rem;
  text-decoration: none;
  text-align: center;
  padding: 8px 10px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
  background-color: ${COLOR.gray1};

  ${(props: ITabProps) =>
    props.active &&
    `
      background-color: ${COLOR.primary};
      color: ${COLOR.white};
    `}

  @media ${BREAKPOINTS.lg} {
    font-size: 1.8rem;
    padding: 12px 10px;
  }
`;

const Line = styled.hr`
  border: 3px solid ${COLOR.primary};
`;

export const S = {
  Tabs,
  Tab,
  Line,
};
