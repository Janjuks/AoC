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