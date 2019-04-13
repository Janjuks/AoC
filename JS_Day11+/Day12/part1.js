const puzzleInputString =  require('./input');

const numberRgx = /-?\d+/g;
const sum = puzzleInputString.match(numberRgx)
    .map(n => +n)
    .reduce((acc, val) => acc + val);

  console.log(sum);
