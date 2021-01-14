import styled from 'styled-components';
import { BREAKPOINTS, COLOR, utilities } from '../../util/variables';

interface ITabProps {
  active: boolean;
}

const Tabs = styled.ul`
  display: inline-flex;
  margin-top: 10px;
  color: ${COLOR.secondary};
  border-bottom: 5px ${COLOR.secondary} solid;
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
  background-color: ${COLOR.primary};

  ${(props: ITabProps) =>
    props.active &&
    `
      background-color: ${COLOR.secondary};
      color: ${COLOR.primaryDark};
    `}

  @media ${BREAKPOINTS.lg} {
    font-size: 1.8rem;
    padding: 12px 10px;
  }
`;

export const S = {
  Tabs,
  Tab,
};
