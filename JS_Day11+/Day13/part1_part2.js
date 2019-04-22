const puzzleInput = require('./input');

const rgx = /^(\w+).+?(gain|lose) (\d+).+?(\w+)\.$/;

const options = puzzleInput.split('\n')
  .map(l => {
    const [_, source, gain, value, destination] = l.match(rgx);
    return {
      pair: [source[0], destination[0]].sort().join(''),
      value: gain === 'gain' ? +value : -value
    };
  })
  .reduce((res, opt) => {
    res[opt.pair] = res[opt.pair] || 0;
    res[opt.pair] += opt.value;
    return res;
  }, {});

const initialsExceptFirst = Array.from(
  new Set(
    Object.keys(options)
      .reduce((res, pair) => res + pair, '')
      .split('')
      .filter(x => x !== 'A')
  )
)

console.log(solve());
console.log(solve2());

function solve2() {
  addMe();
  return solve();
}

function addMe() {
  initialsExceptFirst.forEach(initial => {
    options[initial + 'X'] = 0;
  });
  options['AX'] = 0;
  initialsExceptFirst.push('X');
}

function solve() {
  const seatingValues = getSeatingPermutations()
    .map(pairs => pairs.reduce((acc, pair) => acc + options[pair.split('').sort().join('')], 0));

  return Math.max(...seatingValues);
}

function getSeatingPermutations() {
  return Array.from(permute(initialsExceptFirst))
    .map(x => x.join(''))
    .map(toPairs);
}

function toPairs(str) {
  const res = [];
  for (let i = 0; i <= str.length; i++) {
    const from = str[i - 1] || 'A';
    const to = str[i] || 'A';
    res.push(from + to);
  }

  return res;
}

function* permute(a, n = a.length) {
  if (n <= 1) yield a.slice();
  else for (let i = 0; i < n; i++) {
    yield* permute(a, n - 1);
    const j = n % 2 ? 0 : i;
    [a[n - 1], a[j]] = [a[j], a[n - 1]];
  }
}