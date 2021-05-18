using System.Text.RegularExpressions;

namespace SASS_Splitter {
    public class StyleCloseSCSSBlock : LineSCSSBlock {
        public static bool Test(string line) { return Regex.IsMatch(line,@"^\s*\}\s*$"); }
        public StyleCloseSCSSBlock(string source) : base(source) { }
    }
}