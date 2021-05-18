using System.Text.RegularExpressions;

namespace SASS_Splitter {
    public interface IStubSCSSBlock { }

    internal class StubSCSSBlock : LineSCSSBlock, IStubSCSSBlock {
        public static bool Test(string line) {
            return
                string.IsNullOrWhiteSpace(line) ||
                Regex.IsMatch(line, @"^\s*//.*$") ||
                Regex.IsMatch(line, @"^\s*/\*.*$")
                ;
        }

        public StubSCSSBlock(string line) : base(line) { }
    }

    internal class StubGroupSCSSBlock : GroupSCSSBlock, IStubSCSSBlock {
        public StubGroupSCSSBlock(SCSSBlock seed) : base(seed) { }

        public override bool Append(SCSSBlock primitive) {
            if (primitive is IStubSCSSBlock) {
                Children.Add(primitive);
                return true;
            }

            return false;
        }
    }
}