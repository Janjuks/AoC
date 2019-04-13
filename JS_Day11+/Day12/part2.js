const puzzleInputString = require('./input');
const numberRgx = /-?\d+/g;

const solve = input => {
  while (input.indexOf(':"red"') >= 0) {
    input = removeRed(input);
  }

  const sum = input.match(numberRgx)
    .map(n => +n)
    .reduce((acc, val) => acc + val);

  return sum;
}

const removeRed = input => {
  const redIndex = input.indexOf(':"red"');
  const firstPart = input.substring(0, redIndex);
  const secondPart = input.substring(redIndex);
  let firstPartOk;
  let secondPartOk;

  let bracketsNeeded = 1;

  for (let i = firstPart.length -1; i >= 0; i--) {
    const char = firstPart[i];

    if (char === '}')
      bracketsNeeded++;

    if (char === '{')
      bracketsNeeded--;

    if (bracketsNeeded === 0) {
      firstPartOk = firstPart.substring(0, i);
      break;
    }
  }

  bracketsNeeded = 1;

  for (let i = 0; i < secondPart.length; i++) {
    const char = secondPart[i];

    if (char === '{')
      bracketsNeeded++;

    if (char === '}')
      bracketsNeeded--;

    if (bracketsNeeded === 0) {
      secondPartOk = secondPart.substring(i + 1);
      break;
    }
  }

  const res = firstPartOk + secondPartOk;
  return res;
}

console.log(solve(puzzleInputString));