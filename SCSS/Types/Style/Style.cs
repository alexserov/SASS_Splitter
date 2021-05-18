namespace SASS_Splitter {
    public class StyleSCSSBlock : GroupSCSSBlock {
        public override bool Append(SCSSBlock primitive) {
            if (Children.Count > 0 && Children[^1] is StyleCloseSCSSBlock)
                return false;
            return base.Append(primitive);
        }

        public StyleSCSSBlock(SCSSBlock seed) : base(seed) { }
    }
}