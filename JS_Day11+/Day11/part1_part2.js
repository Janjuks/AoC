const puzzleInput = 'vzbxkghb';

const solve = input => {
  while (!hasStraightAndPairs(input) || hasIllegalChar(input)) {
    input = wrapIllegalChar(input);
    input = wrapChar(input);
  }

  return input;
}

const hasStraightAndPairs = input => hasStraight(input) && hasPairs(input);

const hasStraight = input => {
  let previousCharCode = input.charCodeAt(0);
  let straightCount = 1;

  for (let i = 1; i < input.length; i++) {
    const charCode = input.charCodeAt(i);

    if (previousCharCode + 1 === charCode)
      straightCount++;
    else
      straightCount = 1;

    if (straightCount === 3)
      return true;

    previousCharCode = charCode;
  }

  return false;
}

const hasPairs = input => {
  let previousChar = input[0];
  const pairs = [];

  for (let i = 1; i < input.length; i++) {
    if (input[i] === previousChar)
      pairs.push(previousChar + previousChar);

    previousChar = input[i];
  }

  return new Set(pairs).size > 1;
}

const hasIllegalChar = input => input.search(/[iol]/) >= 0;

const wrapIllegalChar = input => {
  const illegalCharIndex = input.search(/[iol]/);

  if (illegalCharIndex >= 0) {
    const charCode = input.charCodeAt(illegalCharIndex);
    const nextChar = String.fromCharCode(charCode + 1);
    return input.substring(0, illegalCharIndex) + nextChar + 'a'.repeat(input.length - 1 - illegalCharIndex)
  }

  return input;
}

const wrapChar = (input, charIndex = undefined) => {
  charIndex = charIndex || input.length - 1;
  const lastCharCode = input.charCodeAt(charIndex);
  const newCharCode = lastCharCode === 122 ? 97 : lastCharCode + 1;
  input = input.substring(0, charIndex) + String.fromCharCode(newCharCode) + input.substring(charIndex + 1);

  return lastCharCode === 122
    ? wrapChar(input, charIndex - 1)
    : input;
}

const part1Solution = solve(puzzleInput);
const part2Solution = solve(wrapChar(part1Solution));

console.log(part1Solution);
console.log(part2Solution);