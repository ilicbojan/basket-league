import styled, { keyframes } from 'styled-components';
import { BREAKPOINTS, COLOR, utilities } from '../util/variables';

interface IProps {
  show: boolean;
}

const modalOpen = keyframes`
  from { opacity: 0 }
  to { opacity: 1}
`;

const Modal = styled.div<IProps>`
  position: fixed;
  z-index: 999;
  left: 0;
  top: 0;
  height: 100%;
  width: 100%;
  overflow: auto;
  background-color: rgba(0, 0, 0, 0.5);
  animation-name: ${modalOpen};
  animation-duration: 0.6s;

  display: ${(props: IProps) => (props.show ? 'flex' : 'none')};
  justify-content: center;
  align-items: center;
`;

const Content = styled.div`
  background-color: ${COLOR.gray1};
  width: 90%;
  box-shadow: ${utilities.shadow};
  border-radius: ${utilities.borderRadius};
  overflow: hidden;

  @media ${BREAKPOINTS.lg} {
    width: 30%;
  }

  @media ${BREAKPOINTS.xl} {
    width: 25%;
  }
`;

const Header = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: ${COLOR.primary};
  padding: 10px 20px 5px 20px;

  & h2 {
    font-size: 1.8rem;
    margin-bottom: 6px;
    color: ${COLOR.secondary};
  }
`;

const Body = styled.div`
  padding: 20px 20px 5px 20px;
`;

const CloseBtn = styled.div`
  color: ${COLOR.white};

  &:hover,
  &:focus {
    color: ${COLOR.secondary};
    text-decoration: none;
    cursor: pointer;
  }
`;

export const S = {
  Modal,
  Content,
  CloseBtn,
  Header,
  Body,
};
