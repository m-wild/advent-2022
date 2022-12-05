<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "05.input.txt");
var input = File.ReadAllText(file);

var puzzle = input.Split("\r\n\r\n")[0].Split("\r\n");
var cmds = input.Split("\r\n\r\n")[1].Split("\r\n");

var parsePuzzle = () =>
{
	var stacks = puzzle.Last()
		.Chunk(4)
		.ToDictionary(x => int.Parse(new string(x).Trim()), _ => new List<char>());

	foreach (var row in puzzle.Reverse().Skip(1))
	foreach (var (crate, i) in row.Chunk(4).Select((x, i) => (new string(x).Trim().TrimStart('[').TrimEnd(']'), i)))
	if (crate != "") 
		stacks[i + 1].Add(crate[0]);

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
