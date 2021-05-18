using System.Text.RegularExpressions;

namespace SASS_Splitter {
    public class StyleSelectorSCSSBlock : LineSCSSBlock {
        public bool HasBracket {
            get {
                return Regex.IsMatch(this.source, @".*\{");
            }
        }

        public static bool Test(string line) { return Regex.IsMatch(line,@"^.*[,\{]\s*$"); }
        public StyleSelectorSCSSBlock(string source) : base(source) { }
    }
}