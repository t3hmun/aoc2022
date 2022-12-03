<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

string[] data = File.ReadAllLines("../day-02-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

var col1Dict = new Dictionary<char, Shape>
{
	{'A', Shape.Rock },
	{'B', Shape.Paper },
	{'C', Shape.Scissors }
};
var col2Dict = new Dictionary<char, Shape>
{
	{'X', Shape.Rock },
	{'Y', Shape.Paper },
	{'Z', Shape.Scissors }
};

var basePoints = new Dictionary<Shape, int>
{
	{ Shape.Rock, 1 },
	{ Shape.Paper, 2 },
	{ Shape.Scissors, 3 },
};

var total = data.Select(d =>
{
	var opponent = col1Dict[d[0]];
	var you = col2Dict[d[2]];
	int outcome = basePoints[you] + CalcPoints(opponent, you);
	return outcome;
}).Sum();

Console.WriteLine($"Part One: {total}");

static int CalcPoints(Shape opp, Shape you)
{
	if (opp == you) return 3;
	if (you == Shape.Rock) return opp == Shape.Paper ? 0 : 6;
	if (you == Shape.Paper) return opp == Shape.Scissors ? 0 : 6;
	if (you == Shape.Scissors) return opp == Shape.Rock ? 0 : 6;
	throw new Exception("oops");
}
enum Shape
{
	Rock, Paper, Scissors
}

