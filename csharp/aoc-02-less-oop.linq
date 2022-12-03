<Query Kind="Statements" />

Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));

string[] data = File.ReadAllLines("../day-02-data.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();

var partOne = data.Select(line =>
{
	var you = line[2];
	var basePoints = you switch
	{
		'X' => 1,
		'Y' => 2,
		'Z' => 3,
		_ => throw new Exception("bad input")
	};

	var score = line switch {
		"A Y" or "B Z" or "C X" => 6,
		"A Z" or "B X" or "C Y" => 0,
		"A X" or "B Y" or "C Z" => 3,
		_ => throw new Exception($"bad input: {line}")
	};
	
	return basePoints + score;
;
}).Sum();


var partTwo = data.Select(line =>
{
	var you = line[2];
	var basePoints = you switch
	{
		'X' => 0,
		'Y' => 3,
		'Z' => 6,
		_ => throw new Exception("bad input")
	};

	var score = line switch
	{
		"A Y" or "B X" or "C Z" => 1,
		"A Z" or "B Y" or "C X" => 2,
		"A X" or "B Z" or "C Y" => 3,
		_ => throw new Exception($"bad input: {line}")
	};

	return basePoints + score;
	;
}).Sum();

Console.WriteLine($"Part One: {partOne}, Part Two: {partTwo}");

