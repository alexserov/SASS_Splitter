namespace SASS_Splitter {
    public class UseSCSSBlock : LineSCSSBlock {
        public UseSCSSBlock(string source) : base(source) { }

        public static bool Test(string line) { return line.StartsWith("@use"); }

    }
}