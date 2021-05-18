using System.Text.RegularExpressions;

namespace SASS_Splitter {
    public class VariableSCSSBlock : LineSCSSBlock {
        public VariableSCSSBlock(string source) : base(source) { }

        public static bool Test(string line) { return Regex.IsMatch(line,@"^\s*\$.*$"); }

    }
}