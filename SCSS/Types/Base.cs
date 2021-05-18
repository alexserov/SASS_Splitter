using System;
using System.Collections.Generic;
using System.Text;

namespace SASS_Splitter {
    public abstract class SCSSBlock {
        public GroupSCSSBlock Parent { get; set; }
        public abstract void Write(StringBuilder builder);
    }

    public abstract class LineSCSSBlock : SCSSBlock {
        protected readonly string source;
        protected LineSCSSBlock(string source) { this.source = source; }
        public override void Write(StringBuilder builder) { builder.Append(this.source+"\n"); }
    }

    public abstract class GroupSCSSBlock : SCSSBlock {
        public GroupSCSSBlock(SCSSBlock seed) {
            Children = new List<SCSSBlock>();
            Append(seed);
        }

        public virtual bool Append(SCSSBlock primitive) {
            Children.Add(primitive);
            if (primitive.Parent != null)
                throw new Exception();
            primitive.Parent = this;
            return true;
        }
        public List<SCSSBlock> Children { get; }
        public override void Write(StringBuilder builder) {
            foreach (var child in Children) child.Write(builder);
        }
    }
}