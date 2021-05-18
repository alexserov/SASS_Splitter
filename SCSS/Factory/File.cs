using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SASS_Splitter {
    public delegate void ReplaceBlock(SCSSBlock source, params SCSSBlock[] replacement);
    public class SCSSFile {
        readonly List<SCSSBlock> blocks;
        readonly string stringSource;

        public SCSSFile(string source) : this(source, SCSSFileSource.GetInputStream(source), true) { }

        public SCSSFile(string stringSource, StreamReader source, bool disposeSource) : this(stringSource, source) {
            if (disposeSource)
                source.Dispose();
        }

        public SCSSFile(string stringSource, StreamReader source) {
            this.stringSource = stringSource;
            this.blocks = SCSSBlockFactory.Parse(source);
        }

        public void Patch(Action<SCSSBlock, ReplaceBlock> updater) {
            PatchImpl(updater, this.blocks, null);
        }

        public void PatchImpl(Action<SCSSBlock, ReplaceBlock> updater, List<SCSSBlock> collection, GroupSCSSBlock parent) {
            for (var i = 0; i < collection.Count; i++) {
                var current = collection[i];
                var currentCount = collection.Count;
                updater(current, (o, n) => {
                    var index = collection.IndexOf(o);
                    o.Parent = null;
                    collection.RemoveAt(index);
                    for (int i = 0; i < n.Length; i++) {
                        n[i].Parent = parent;
                        collection.Insert(index + i, n[i]);
                    }
                });
                var newCount = collection.Count;
                var diff = currentCount - newCount;
                if (diff != 0) {
                    if (diff > 1)
                        throw new Exception();
                    i += diff;
                }

                if (current == collection[i] && current is GroupSCSSBlock)
                    PatchImpl(updater, ((GroupSCSSBlock) current).Children, (GroupSCSSBlock) current);
            }
        }

        public void Write(string path = null) {
            var filePath = path;
            if (string.IsNullOrEmpty(filePath))
                filePath = this.stringSource;
            if (string.IsNullOrEmpty(filePath))
                filePath = path;
            if (!File.Exists(filePath))
                throw new InvalidOperationException("Incorrect file path");
            using (var writer = new StreamWriter(filePath, false)) {
                var sb = new StringBuilder();
                foreach (var block in this.blocks) {
                    block.Write(sb);
                }
                writer.Write(sb.ToString());
            }
        }
    }
}