namespace Demo.Common.Models
{
    public class TableInfo
    {
        public Point Location { get; set; } = new Point();
        public IDictionary<string, string>? Headers { get; set; } = new Dictionary<string, string>();
        public IEnumerable<IDictionary<string, object>> Values { get; set; } = new List<Dictionary<string, object>>();
    }

    public struct Point
    {
        public int X; 
        public int Y;
    }
}
