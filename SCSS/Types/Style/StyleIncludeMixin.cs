using System.Text.RegularExpressions;

namespace SASS_Splitter {
    public class StyleIncludeMixinSCSSBlock : LineSCSSBlock {
        public static bool Test(string line) { return Regex.IsMatch(line,@"^\s*@include.*;\s*$"); }
        public StyleIncludeMixinSCSSBlock(string source) : base(source) { }
    }
}