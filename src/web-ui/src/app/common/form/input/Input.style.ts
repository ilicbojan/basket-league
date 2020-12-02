import styled from 'styled-components';
import { COLOR, utilities } from '../../util/variables';

interface IProps {
  block?: boolean;
  width?: any;
}

const Input = styled.input<IProps>`
  font-size: 1.6rem;
  padding: 10px;
  outline: none;
  border: 2px solid ${COLOR.white};
  border-radius: ${utilities.borderRadius};
  box-shadow: ${utilities.shadow};

  width: ${(props: IProps) => props.block && '100%'};

  &:focus {
    border-color: ${COLOR.primary};
  }
`;

const FormItem = styled.div<IProps>`
  margin: 15px 0 0 0;
  width: ${(props: IProps) => props.block && '100%'};

  label {
    display: block;
  }
`;

const Error = styled.span`
  font-size: 1.4rem;
  color: red;
`;

export const S = {
  Input,
  FormItem,
  Error,
};
