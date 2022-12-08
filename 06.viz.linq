<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "06.input.txt");
var input = File.ReadAllLines(file);

const int LEN_PKG = 4;
const int LEN_MSG = 14;

var fmt = (string datastream, int marker, int index, bool hit) =>
{
	var color = hit ? "lightgreen" : "red";
	
	var html = "<html>";
	html += "<style>p{ max-width:800px; word-wrap:break-word; font-family:'Berkeley Mono'; }</style>";
	html += $"<style>span{{ color:{color}; font-weight:700; }}</style>";
	html += "<p>";
	html += datastream.Substring(0, index);
	html += "<span>";
	html += datastream.Substring(index, marker);
	html += "</span>";
	html += datastream.Substring(index + marker);
	html += "</p></html>";
	
	return Util.RawHtml(html);
};

var findHeader = async (string datastream, int marker) =>
{
	var dc = new DumpContainer().Dump();
	
	for (var i = 0; i < datastream.Length - marker; i++)
	{
		dc.Content = fmt(datastream, marker, i, false);
		await Task.Delay(1);

		var packet = datastream.Substring(i, marker);
		if (packet.Distinct().Count() == marker)
		{
			dc.Content = fmt(datastream, marker, i, true);

			return i + marker;
		}
	}
	return 0;
};

foreach (var datastream in input)
{
	findHeader(datastream, LEN_PKG).Dump();
	findHeader(datastream, LEN_MSG).Dump();
}