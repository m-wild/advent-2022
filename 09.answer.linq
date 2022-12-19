<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

var file = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Util.CurrentQueryPath), "09.input.txt");
var input = File.ReadAllLines(file);

var moves = input
	.Select(i => new
	{
		Direction = i.Split(' ')[0][0],
		Distance = int.Parse(i.Split(' ')[1])
	})
	.ToList();
	

//var rope = new List<Knot>
//{
//	new('H'),
//	new('T'),
//};

var rope = new List<Knot>
{
	new('H'),
	new('1'),
	new('2'),
	new('3'),
	new('4'),
	new('5'),
	new('6'),
	new('7'),
	new('8'),
	new('9'),
};


const int scale = 1;
const int size = 400;
const int centerX = 300;
const int centerY = 300;

var canvas = new Canvas
{
	Height = size,
	Width = size,
	Background = Brushes.Black,
};
canvas.Dump();

var ropeRects = rope.ToDictionary(
	k => k,
	k => new Rectangle
	{
		Height = scale,
		Width = scale,
		Fill = k.C == 'H' ? Brushes.OrangeRed : Brushes.LightCoral,
	});

foreach (var rect in ropeRects.Values)
{
	canvas.Children.Add(rect);
	Canvas.SetLeft(rect, centerX);
	Canvas.SetTop(rect, centerY);
}

var visitedRects = new Dictionary<(int x, int y), Rectangle>();

void updateCanvas()
{
	foreach (var (knot, rect) in ropeRects)
	{
		Canvas.SetLeft(rect, centerX + knot.X * scale);
		Canvas.SetTop(rect, centerY + knot.Y * scale);
	}
	
	foreach (var p in rope.Last().Visited.Except(visitedRects.Keys))
	{
		var rect = new Rectangle
		{
			Height = scale,
			Width = scale,
			Fill = Brushes.IndianRed,
		};
		canvas.Children.Add(rect);
		Canvas.SetLeft(rect, centerX + p.x * scale);
		Canvas.SetTop(rect, centerY + p.y * scale);
		visitedRects.Add(p, rect);
	}
}


for (var i = 0; i < moves.Count; i++)
for (var j = 0; j < moves[i].Distance; j++)
{
	rope[0].Move(moves[i].Direction);
	
	for (int k = 1; k < rope.Count; k++)
		rope[k].Follow(rope[k-1]);
	
//	if (i % 4 == 0)
//		await Task.Delay(1);
//
//	//await Task.Delay(100);
//	
//	updateCanvas();
}
updateCanvas();

rope.Last().Visited.Count.Dump("Tail visited:");

class Knot
{
	public Knot(char c)
	{
		this.X = 0;
		this.Y = 0;
		this.C = c;
		this.Visited = new() { (0, 0) };
	}

	public int X { get; private set; }
	public int Y { get; private set; }
	public char C { get; private set; }
	public HashSet<(int x, int y)> Visited { get; } = new();

	public void Move(char direction)
	{
		X += direction == 'R' ? 1 : direction == 'L' ? -1 : 0;
		Y += direction == 'D' ? 1 : direction == 'U' ? -1 : 0;
		
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