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

export const hours = [
  '12:00:00',
  '13:00:00',
  '14:00:00',
  '15:00:00',
  '16:00:00',
  '17:00:00',
  '18:00:00',
  '19:00:00',
  '20:00:00',
  '21:00:00',
  '22:00:00',
  '23:00:00',
];
