namespace SASS_Splitter {
    public class StyleSelectorGroupSCSSBlock : GroupSCSSBlock {
        public override bool Append(SCSSBlock primitive) {
            if (primitive is StyleSelectorSCSSBlock) {
                if (Children.Count == 0 || !(Children[Children.Count - 1] as StyleSelectorSCSSBlock).HasBracket)
                    return base.Append(primitive);
            }

            return false;
        }

        public StyleSelectorGroupSCSSBlock(SCSSBlock seed) : base(seed) { }
    }
}