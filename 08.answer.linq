<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "08.input.txt");
var input = File.ReadAllLines(file);

const int LEFT = 0, RIGHT = 1, DOWN = 2, UP = 3;

var len = input.Length;
var trees = new int[len, len];

for (var y = 0; y < len; y++)
for (var x = 0; x < len; x++)
	trees[y,x] = (int)char.GetNumericValue(input[y][x]);

var vis = (int t, int x, int y, int direction) =>
{
	var i = direction switch 
	{
		UP    => y - 1, DOWN  => y + 1,
		LEFT  => x - 1, RIGHT => x + 1,
	};
	
	while (direction is UP or LEFT ? i >= 0 : i < len)
	{
		var yy = direction is UP or DOWN ? i : y;
		var xx = direction is LEFT or RIGHT ? i : x;
		
		if (trees[yy,xx] >= t) return false;
		
		i += direction is UP or LEFT ? -1 : 1;
	}
	return true;
};

var isVisible = (int x, int y) =>
{
	if (x == 0 || y == 0 || x + 1 == len || y + 1 == len) return true;
	var t = trees[y,x];
	return vis(t,x,y,LEFT) || vis(t,x,y,RIGHT) || vis(t,x,y,UP) || vis(t,x,y,DOWN);
};

var map = new object[len, len];
var count = 0;

for (var y = 0; y < len; y++)
for (var x = 0; x < len; x++)
{
	var visible = isVisible(x, y);
	if (visible) count++;
	
	map[y,x] = Util.HighlightIf(visible, "darkgreen", trees[y,x]);
}

map.Dump();
count.Dump();

Debug.Assert(count == 1713);


var dist = (int t, int x, int y, int direction) =>
{
	var count = 0;
	var i = direction switch 
	{
		UP    => y - 1, DOWN  => y + 1,
		LEFT  => x - 1, RIGHT => x + 1,
	};
	
	while (direction is UP or LEFT ? i >= 0 : i < len)
	{
		count++;

		var yy = direction is UP or DOWN ? i : y;
		var xx = direction is LEFT or RIGHT ? i : x;
		
		if (trees[yy,xx] >= t) break;
		
		i += direction is UP or LEFT ? -1 : 1;
	}
	return count;
};

var score = (int x, int y) =>
{
	var t = trees[y, x];
	return dist(t, x, y, LEFT) * dist(t, x, y, RIGHT) * dist(t, x, y, UP) * dist(t, x, y, DOWN);
};

var maxScore = 0;

for (var y = 0; y < len; y++)
for (var x = 0; x < len; x++)
{
	var s = score(x, y);
	maxScore = s > maxScore ? s : maxScore;
}

maxScore.Dump();
Debug.Assert(maxScore == 268464);