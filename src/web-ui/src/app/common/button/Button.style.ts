import styled, { keyframes } from 'styled-components';
import { COLOR, utilities } from '../util/variables';

interface IProps {
  block?: boolean;
  color?: string;
  width?: any;
}

const Button = styled.button`
  width: ${(props: IProps) => (props.block ? '100%' : props.width + 'px')};
  height: 45px;

  position: relative;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  padding: 0 24px;
  font-size: 1.5rem;
  text-transform: uppercase;
  text-decoration: none;
  text-align: center;
  font-weight: bold;
  letter-spacing: 1px;
  cursor: pointer;
  box-shadow: ${utilities.shadow};
  border-radius: ${utilities.borderRadius};
  overflow: hidden;
  transition: background-color 0.4s;

  ${({ color }) =>
    color === 'primary' &&
    `
      background-color: ${COLOR.primary};
      color: ${COLOR.white};
    `}

  ${({ color }) =>
    color === 'secondary' &&
    `
      background-color: ${COLOR.secondary};
      color: ${COLOR.white};
    `}

  ${({ color }) =>
    color === 'red' &&
    `
      background-color: ${COLOR.red};
      color: ${COLOR.white};
    `}

  &:hover,
  &:focus {
    ${({ color }) =>
      color === 'primary' &&
      `
      background-color: ${COLOR.primaryDark};
    `}

    ${({ color }) =>
      color === 'secondary' &&
      `
      background-color: ${COLOR.secondaryDark};
    `}

    ${({ color }) =>
      color === 'red' &&
      `
      background-color: ${COLOR.red};
    `}
  }

  &:disabled,
  &[disabled] {
    ${({ color }) =>
      color === 'primary' &&
      `
      background-color: ${COLOR.primaryLight};
      color: ${COLOR.primaryLight};
    `}

    ${({ color }) =>
      color === 'secondary' &&
      `
      background-color: ${COLOR.secondaryDark};
      color: ${COLOR.secondaryDark};
    `}

    ${({ color }) =>
      color === 'red' &&
      `
      background-color: ${COLOR.red};
      color: ${COLOR.red};
    `}
  }

  & svg {
    margin-right: 6px;
  }
`;

const spinner = keyframes`
  to {
    transform: rotate(360deg);
  }
`;

const Spinner = styled.span`
  &:before {
    content: '';
    position: absolute;
    top: 50%;
    left: 50%;
    height: 3.2rem;
    width: 3.2rem;
    margin-top: -1.6rem;
    margin-left: -1.6rem;
    border-radius: 50%;
    border: 4px solid gray;
    border-top-color: ${COLOR.secondary};
    animation: ${spinner} 0.7s linear infinite;
  }
`;

export const S = {
  Button,
  Spinner,
};
