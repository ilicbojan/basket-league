import styled from 'styled-components';
import { COLOR, utilities } from '../../util/variables';

export interface IProps {
  block?: boolean;
  width?: any;
}

const Select = styled.select<IProps>`
  font-size: 1.6rem;
  padding: 10px;
  outline: none;
  border: 1px solid ${COLOR.white};
  border-radius: ${utilities.borderRadius};
  box-shadow: ${utilities.shadow};
  background-color: ${COLOR.white};

  height: 40px;
  width: ${(props: IProps) => props.block && '100%'};

  &:focus {
    border-color: ${COLOR.primary};
  }

  &:disabled,
  &[disabled] {
    background-color: ${COLOR.gray};
    border: 1px solid ${COLOR.gray};
  }
`;

const FormItem = styled.div`
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
  Select,
  FormItem,
  Error,
};
