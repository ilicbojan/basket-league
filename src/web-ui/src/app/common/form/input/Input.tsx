import React from 'react';
import { FieldRenderProps } from 'react-final-form';
import { S } from './Input.style';

interface IProps extends FieldRenderProps<any, HTMLElement> {
  label?: string;
  block?: boolean;
}

const Input: React.FC<IProps> = ({
  input,
  meta: { touched, error },
  label,
  block,
  ...props
}) => {
  return (
    <S.FormItem>
      <label>{label}</label>
      <S.Input {...props} {...input} block={block} />
      {error && touched && <S.Error>{error}</S.Error>}
    </S.FormItem>
  );
};

export default Input;
