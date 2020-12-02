import React from 'react';
import { FieldRenderProps } from 'react-final-form';
import { S } from './Select.style';

interface IProps extends FieldRenderProps<string, HTMLElement> {
  label?: string;
  block?: boolean;
  disabled?: boolean;
  empty?: boolean;
}

const Select: React.FC<IProps> = ({
  input,
  width,
  label,
  block,
  children,
  required,
  disabled,
  meta: { touched, error },
}) => {
  return (
    <S.FormItem>
      <label>{label}</label>
      <S.Select
        {...input}
        block={block}
        width={width}
        required={required}
        disabled={disabled}
      >
        <option value='' disabled hidden></option>
        {children}
      </S.Select>
      {error && touched && <S.Error>{error}</S.Error>}
    </S.FormItem>
  );
};

export default Select;
