
var canvas = document.getElementById("myCanvas");
var ctx = canvas.getContext("2d");
var img = document.getElementById("ant")


function getRandomInt(min=0, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

class Ameise {
	constructor(x, y) {
		this.x = x;
		this.y = y;
	}

	spawn() {
		this.x = getRandomInt(0, 800);
		this.y = getRandomInt(0, 600);
	}

	move() {
		ctx.clearRect(this.x, this.y, 20, 20); //cleaning

		var stepX = getRandomInt(-5, 5);
		var stepY = getRandomInt(-5, 5);

		this.x = this.x + stepX;
		this.y = this.y + stepY;

		ctx.drawImage(ant, this.x, this.y);
}
}

let ameisen = [];
let ameisenAmount = 5;

for (var i = 0; i < ameisenAmount; i++) {
	ameisen[i] = new Ameise();
	ameisen[i].spawn();
	console.log("Ameise " + i + " lebt.")
}

let a01 = new Ameise(200, 200);
a01.spawn();

async function update() {
	// await sleep(500);
	// a01.move();
	for (var i = 0; i < ameisenAmount; i++) {
	
	ameisen[i].move();
	console.log("Position Ameise " + i + ameisen[i].x + " " + ameisen[i].y)
	}
	await sleep(100);
	update();
}



function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}






















// let lastTime = 0;
// function update(time = 0) {
// 	const deltaTime = time - lastTime;
// 	lastTime = time;
// 	console.log(time);
// 	spawn();
// 	requestAnimationFrame(update);
// }

// update();
