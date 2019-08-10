const puzzleInput = require('./input');
const checkPoint = 2503;
const rgx = /^(\w+).+?(\d+).+?(\d+).+?(\d+)/;

const testInput = `Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.`;

const reindeers = puzzleInput.split('\n')
  .map(l => {
    const [_, name, speed, travelTime, restTime] = l.match(rgx);
    return {
      name: name,
      speed: +speed,
      travelTime: +travelTime,
      restTime: +restTime
    };
  });

console.log(solve());
console.log(solve2());

function solve() {
  const distances = reindeers.map(getDistanceTravelled);
  return Math.max(...distances);
}

function getDistanceTravelled(reindeer) {
  let distanceTravelled = 0;
  let timePassed = 0;
  let wasTravelling = false;

  while (timePassed < checkPoint) {
    if (wasTravelling)
      timePassed += reindeer.restTime;
    else {
      distanceTravelled += reindeer.speed * reindeer.travelTime;
      timePassed += reindeer.travelTime;
    }

    wasTravelling = !wasTravelling;
  }

  if (wasTravelling)
    distanceTravelled -= (timePassed - checkPoint) * reindeer.speed;

  return distanceTravelled;
}

/* Part 2 */

function solve2() {
  const reindeerRaceRecords = reindeers.map(r => ({ name: r.name, record: getRaceRecord(r) }));
  const reindeerPoints = reindeers.map(r => ({ name: r.name, points: 0 }));

  for (let i = 1; i <= checkPoint; i++) {
    const reindeerDistances = reindeerRaceRecords.map(r => ({ name: r.name, distance: getReindeerDistanceAt(r.record, i) }))
      .sort((a, b) => b.distance - a.distance);

    const winner = reindeerDistances[0];
    const winners = reindeerDistances.filter(x => x.distance === winner.distance);

    reindeerPoints.forEach(r => {
      if (winners.some(w => w.name === r.name))
        r.points++;
    });
  }

  return Math.max(...reindeerPoints.map(r => r.points));
}

function getRaceRecord(reindeer) {
  let res = [];
  let timePassed = 0;
  let isResting = false;

  while (timePassed < checkPoint) {
    const distanceTravelled = (res[res.length - 1] || [0, 0])[1];

    if (isResting) {
      res = res.concat(Array.from({ length: reindeer.restTime }, (v, i) => [i + 1 + timePassed, distanceTravelled]));
      timePassed += reindeer.restTime;
    }
    else {
      res = res.concat(Array.from({ length: reindeer.travelTime }, (v, i) => [i + 1 + timePassed, distanceTravelled + (reindeer.speed * (i + 1))]));
      timePassed += reindeer.travelTime;
    }

    isResting = !isResting;
  }

  return res;
}

function getReindeerDistanceAt(raceRecord, time) {
  return raceRecord.find(([_time, distance]) => _time === time)[1];
}