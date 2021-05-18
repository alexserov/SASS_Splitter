using System;
using System.Collections.Generic;
using System.IO;

namespace SASS_Splitter {
    internal class SCSSBlockFactory {
        static GroupSCSSBlock ParseGroup(SCSSBlock current) {
            if (current is StubSCSSBlock) {
                return new StubGroupSCSSBlock(current);
            }

            if (current is StyleSelectorSCSSBlock)
                return new StyleSelectorGroupSCSSBlock(current);

            if (current is StyleSelectorGroupSCSSBlock)
                return new StyleSCSSBlock(current);

            return null;
        }

        static LineSCSSBlock ParsePrimitive(string line) {
            if (StyleSelectorSCSSBlock.Test(line))
                return new StyleSelectorSCSSBlock(line);
            if (StyleCloseSCSSBlock.Test(line))
                return new StyleCloseSCSSBlock(line);
            if (VariableSCSSBlock.Test(line))
                return new VariableSCSSBlock(line);
            if (UseSCSSBlock.Test(line))
                return new UseSCSSBlock(line);
            if (StylePropertySCSSBlock.Test(line))
                return new StylePropertySCSSBlock(line);
            if (StubSCSSBlock.Test(line))
                return new StubSCSSBlock(line);
            if (StyleIncludeMixinSCSSBlock.Test(line))
                return new StyleIncludeMixinSCSSBlock(line);
            throw new InvalidOperationException();
        }

        public static List<SCSSBlock> Parse(StreamReader source) {
            List<SCSSBlock> lines = new List<SCSSBlock>();
            while (source.Peek() != -1) {
                lines.Add(ParsePrimitive(source.ReadLine()));
            }
            
            bool hasGroupings = false;
            do {
                hasGroupings = false;
                for (int i = lines.Count-1; i >=0; i--) {
                    var current = lines[i];
                    var group = ParseGroup(current);
                    if(group==null)
                        continue;
                    lines[i] = group;
                    int j = 0;
                    while (i + j + 1 < lines.Count) {
                        if (!group.Append(lines[i + j + 1]))
                            break;
                        lines.RemoveAt(i+j+1);
                        hasGroupings = true;
                    }
                }
            } while (hasGroupings);

            return lines;
        }
    }
}