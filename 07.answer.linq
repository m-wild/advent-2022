<Query Kind="Statements" />

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "07.sample.txt");
var input = File.ReadAllLines(file);

var cwd = new Stack<string>();
var fs = new Dictionary<string, Dir>
{
	["/"] = new Dir(),
};

for (var i = 0 ; i < input.Length; i++)
{
	if (input[i].StartsWith("$ cd "))
	{
		var arg = input[i].Substring(5);
		if (arg == "/") { cwd.Clear(); }
		else if (arg == "..") { cwd.Pop(); }
		else { cwd.Push(arg); }
	}
	else if (input[i].StartsWith("$ ls"))
	{
		var path = "/" + string.Join("/", cwd.Reverse());
		
		if (!fs.ContainsKey(path))
		{
			var dir = new Dir { path = path };
			
			var parentPath = path.Substring(0, path.LastIndexOf('/'));
			var parent = fs[parentPath == "" ? "/" : parentPath];
			parent.children.Add(dir);
			
			fs[path] = dir;
		}
		
		while(i + 1 < input.Length && !input[i + 1].StartsWith('$'))
		{
			var x = input[++i].Split(' ');
			if (x[0] != "dir")
			{
				checked
				{
					fs[path].size += long.Parse(x[0]);
				}
			}
		}
	}
}

fs.Select(kv => new { Path = kv.Key, kv.Value.TotalSize }).Dump()
	.Where(x => x.TotalSize <= 100000)
	.Sum(x => x.TotalSize)
	.Dump();


class Dir
{
	public string path = "/";
	public long size = 0;
	public List<Dir> children = new();


	public long TotalSize
	{
		get
		{
			checked
			{
				return size + children.Sum(c => c.TotalSize);
			}
		}
	}
}