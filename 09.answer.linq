<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "09.sample2.txt");
var input = File.ReadAllLines(file);

var moves = input
	.Select(i => new
	{
		Direction = i.Split(' ')[0],
		Distance = int.Parse(i.Split(' ')[1])
	})
	.ToList();
	
const int len = 40;
var itl = 19;

var rope = new List<Knot>
{
	new(itl, itl, 'H'),
	new(itl, itl, '1'),
	new(itl, itl, '2'),
	new(itl, itl, '3'),
	new(itl, itl, '4'),
	new(itl, itl, '5'),
	new(itl, itl, '6'),
	new(itl, itl, '7'),
	new(itl, itl, '8'),
	new(itl, itl, '9'),
};



var grid = new object[len, len];
var dc = new DumpContainer().Dump();
void dumpGrid()
{
	for (var y = 0; y < len; y++)
	for (var x = 0; x < len; x++)
		grid[y, x] = rope.Last().Visited.Contains((x, y)) ? '#' : ' ';

	foreach (var knot in Enumerable.Reverse(rope))
		grid[knot.Y, knot.X] = Util.Highlight(knot.C);

	dc.UpdateContent(grid);
}



foreach (var m in moves)
for (var i = 0; i < m.Distance; i++)
{
	rope[0].Move(m.Direction);
	
	for (int k = 1; k < rope.Count; k++)
		rope[k].Follow(rope[k-1]);
	
	dumpGrid();
	Thread.Sleep(50);
}
dumpGrid();

rope.Last().Visited.Count.Dump("Tail visited:");


class Knot
{
	public Knot(int x, int y, char c)
	{
		this.X = x;
		this.Y = y;
		this.C = c;
		this.Visited = new() { (x, y) };
	}

	public int X { get; private set; }
	public int Y { get; private set; }
	public char C { get; private set; }
	public HashSet<(int x, int y)> Visited { get; } = new();

	public void Move(string direction)
	{
		X += direction == "R" ? 1 : direction == "L" ? -1 : 0;
		Y += direction == "D" ? 1 : direction == "U" ? -1 : 0;
		
		Visited.Add((X, Y));
	}
	
	public void Follow(Knot k)
	{
		var dx = X - k.X;
		var dy = Y - k.Y;

		X += dx >  1 || (dx > 0 && Math.Abs(dy) > 1) ? -1 : 0;
		X += dx < -1 || (dx < 0 && Math.Abs(dy) > 1) ?  1 : 0;

		Y += dy >  1 || (dy > 0 && Math.Abs(dx) > 1) ? -1 : 0;
		Y += dy < -1 || (dy < 0 && Math.Abs(dx) > 1) ?  1 : 0;
		
		Visited.Add((X,Y));
	}
	
}