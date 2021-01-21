const size = {
  xs: '480px',
  sm: '768px',
  md: '992px',
  lg: '1200px',
  xl: '1440px',
};

// DESKTOP FIRST
// export const BREAKPOINTS = {
//   xs: `(max-width: ${size.xs})`,
//   sm: `(max-width: ${size.sm})`,
//   md: `(max-width: ${size.md})`,
//   lg: `(max-width: ${size.lg})`,
//   xl: `(max-width: ${size.xl})`,
// };

// MOBILE FIRST
export const BREAKPOINTS = {
  xs: `(min-width: ${size.xs})`,
  sm: `(min-width: ${size.sm})`,
  md: `(min-width: ${size.md})`,
  lg: `(min-width: ${size.lg})`,
  xl: `(min-width: ${size.xl})`,
};

export const COLOR = {
  primary: '#e35000',
  primaryLight: '#e35000',
  primaryDark: '#e35000',
  secondary: '#0B79E3',
  secondaryDark: '#188155',
  red: '#ff1500',
  gray1: '#464646',
  gray2: '#2d2d2d',
  gray3: '#575757',
  grayLight: '#f6f6f6 ',
  grayDark: '#333',
  white: '#fff',
  black: '#000',
};

export const utilities = {
  shadow: '0 5px 8px 0 rgba(0, 0, 0, 0.3)',
  borderRadius: '5px',
  navHeight: '60px',
};
