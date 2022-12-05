<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "05.input.txt");
var input = File.ReadAllText(file);

var puzzle = input.Split(Environment.NewLine + Environment.NewLine)[0].Split(Environment.NewLine);
var cmds = input.Split(Environment.NewLine + Environment.NewLine)[1].Split(Environment.NewLine);

var parsePuzzle = () =>
{
	var stacks = Enumerable.Range(1, (int)Math.Ceiling(puzzle[0].Length / 4.0))
		.ToDictionary(x => x, _ => new List<char>());

	foreach (var x in puzzle.TakeWhile(x => x.Contains('[')))
	for (int i = 0, j = 1; i < stacks.Count * 4; i += 4, j++)
		if (x[i + 1] != ' ') stacks[j].Insert(0, x[i + 1]);
	
	return stacks;
};

var parseCmd = (string x) =>
{
	var parts = x.Split(' ');
	return (
		count: int.Parse(parts[1]),
		from: int.Parse(parts[3]), 
		to: int.Parse(parts[5])
	);
};

// --- Part One ---
var st1 = parsePuzzle();
foreach (var x in cmds)
{
	var (count, from, to) = parseCmd(x);

	var ix = st1[from].Count - count;
	var crates = st1[from].GetRange(ix, count);
	st1[from].RemoveRange(ix, count);
	st1[to].AddRange(crates.AsEnumerable().Reverse());
}

var pt1 = string.Join("", st1.Select(x => x.Value.Last())).Dump();
Debug.Assert(pt1 == "GRTSWNJHH");

// --- Part Two ---
var st2 = parsePuzzle();
foreach (var x in cmds)
{
	var (count, from, to) = parseCmd(x);

	var ix = st2[from].Count - count;
	var crates = st2[from].GetRange(ix, count);
	st2[from].RemoveRange(ix, count);
	st2[to].AddRange(crates);
}

var pt2 = string.Join("", st2.Select(x => x.Value.Last())).Dump();
Debug.Assert(pt2 == "QLFQDBBHM");
