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
  background-color: ${COLOR.gray3};
  border: 1px solid ${COLOR.gray3};
  border-radius: ${utilities.borderRadius};
  box-shadow: ${utilities.shadow};

  height: 40px;
  width: ${(props: IProps) => props.block && '100%'};

  &:focus {
    border-color: ${COLOR.secondary};
  }

  &:disabled,
  &[disabled] {
    background-color: ${COLOR.gray1};
    border: 1px solid ${COLOR.gray1};
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
