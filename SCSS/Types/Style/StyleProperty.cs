using System.Text.RegularExpressions;

namespace SASS_Splitter {
    public class StylePropertySCSSBlock : LineSCSSBlock {
        public string Name { get; set; }
        public string Value { get; set; }
        public static bool Test(string line) { return Regex.IsMatch(line,@"^\s*[\w\d-]+:\s+.*$"); }

        public StylePropertySCSSBlock(string source) : base(source) {
            var expr = new Regex(@"^\s*([\w\d-]+):\s+(.*);$");
            var match = expr.Match(source);
            var name = match.Groups[1];
            var value = match.Groups[2];
        }
    }
}