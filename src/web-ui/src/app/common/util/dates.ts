export const getDate = (date: Date) => {
  const dateString =
    date.getDate() +
    '.' +
    (date.getMonth() + 1) +
    '.' +
    date.getFullYear() +
    '.';

  return dateString;
};

export const getDateAndMonth = (date: Date) => {
  return date.getDate() + '.' + (date.getMonth() + 1) + '.';
};

export const getDateAndTime = (date: Date) => {
  date = new Date(date);

  const dateString = getDate(date);
  const timeString = date.getHours() + ':' + date.getMinutes();

  return dateString + ' - ' + timeString;
};

export const getTime = (time: string) => {
  return time.slice(0, -3);
};
