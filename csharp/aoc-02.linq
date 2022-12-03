<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

string[] data = File.ReadAllLines("../day-02-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

var rock = new ShapeInfo(Shape.Rock, 1, Shape.Scissors, Shape.Paper);
var paper = new ShapeInfo(Shape.Paper, 2, Shape.Rock, Shape.Scissors);
var scissors = new ShapeInfo(Shape.Scissors, 3, Shape.Paper, Shape.Rock);

var shapeMap = new Dictionary<Shape, ShapeInfo>
{
	{Shape.Rock, rock },
	{Shape.Paper, paper },
	{Shape.Scissors, scissors }
};

var oppMap = new Dictionary<char, ShapeInfo>
{
	{'A', rock },
	{'B', paper },
	{'C', scissors }
};

var youMap = new Dictionary<char, ShapeInfo>
{
	{'X', rock },
	{'Y', paper },
	{'Z', scissors }
};

var partOne = data.Select(d =>
{
	var opp = oppMap[d[0]];
	var you = youMap[d[2]];
	int outcome = you.BasePoints + CalcPoints(opp, you);
	return outcome;
}).Sum();

var partTwo = data.Select(d =>
{
	var opp = oppMap[d[0]];
	var you = shapeMap[ShapeForOutcome(opp, d[2])];
	int outcome = you.BasePoints + CalcPoints(opp, you);
	return outcome;
}).Sum();

Console.WriteLine($"Part One: {partOne}, Part Two: {partTwo}");

static int CalcPoints(ShapeInfo opp, ShapeInfo you)
{
	if (you.WinsAgainst == opp.Shape) return 6;
	if (you.LosesAgainst == opp.Shape) return 0;
	return 3;
}

static Shape ShapeForOutcome(ShapeInfo opp, char outcome)
{
	if (outcome == 'X') return opp.WinsAgainst;
	if (outcome == 'Y') return opp.Shape;
	if (outcome == 'Z') return opp.LosesAgainst;
	throw new Exception("Invalid outcome, should be X, Y or Z.");
}

enum Shape
{
	Rock, Paper, Scissors
}

record ShapeInfo(Shape Shape, int BasePoints, Shape WinsAgainst, Shape LosesAgainst);
