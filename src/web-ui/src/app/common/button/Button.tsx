import React from 'react';
import { S } from './Button.style';

interface IProps extends React.HTMLAttributes<HTMLButtonElement> {
  block?: boolean;
  width?: any;
  color?: string;
  loading?: boolean;
  disabled?: boolean;
}

const Button: React.FC<IProps> = ({
  children,
  block,
  width,
  color,
  loading,
  disabled,
  ...props
}) => {
  return (
    <S.Button
      {...props}
      color={color}
      block={block}
      width={width}
      disabled={loading || disabled}
    >
      {loading && <S.Spinner />}
      {children}
    </S.Button>
  );
};

export default Button;
