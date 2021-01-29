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
  background-color: ${COLOR.gray3};
  border: 1px solid ${COLOR.gray3};
  border-radius: ${utilities.borderRadius};
  box-shadow: ${utilities.shadow};

  height: 40px;
  width: ${(props: IProps) => props.block && '100%'};

  &:focus {
    border-color: ${COLOR.secondary};
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
