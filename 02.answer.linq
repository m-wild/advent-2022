<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "02.input.txt");
var input = File.ReadAllLines(file);

const int LOSE = 0, DRAW = 3, WIN = 6;
const int ROCK = 1, PAPER = 2, SCISSORS = 3;

// a, x = rock
// b, y = paper
// c, z = scissors
var pt1 = new Dictionary<string, int>
{
	["A X"] = ROCK + DRAW,
	["B X"] = ROCK + LOSE,
	["C X"] = ROCK + WIN,
	
	["A Y"] = PAPER + WIN,
	["B Y"] = PAPER + DRAW,
	["C Y"] = PAPER + LOSE,
	
	["A Z"] = SCISSORS + LOSE,
	["B Z"] = SCISSORS + WIN,
	["C Z"] = SCISSORS + DRAW,
};

input.Sum(i => pt1[i]).Dump();


// x = lose
// y = draw
// z = win
var pt2 = new Dictionary<string, int>
{
	["A X"] = SCISSORS + LOSE,
	["B X"] = ROCK + LOSE,
	["C X"] = PAPER + LOSE,

	["A Y"] = ROCK + DRAW,
	["B Y"] = PAPER + DRAW,
	["C Y"] = SCISSORS + DRAW,

	["A Z"] = PAPER + WIN,
	["B Z"] = SCISSORS + WIN,
	["C Z"] = ROCK + WIN,
};

input.Sum(i => pt2[i]).Dump();