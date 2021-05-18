using System.IO;

namespace SASS_Splitter {
    internal partial class Program {
        static void Main(string[] args) {
            var dir = @"C:\work\DevExtreme\scss\widgets\material\accordion\_index.scss";
            if (Directory.Exists(dir)) {
                var dirFiles = Directory.GetFiles(dir, "*.scss", SearchOption.AllDirectories);
                foreach (var file in dirFiles) UpdateFile(file);
            } else if (File.Exists(dir)) {
                UpdateFile(dir);
            }
        }

        static void UpdateFile(string fileName) {
            var file = new SCSSFile(fileName);
            file.Patch(ExpandCompexProperties);
            file.Write();
        }

        static void ExpandCompexProperties(SCSSBlock arg1, ReplaceBlock arg2) {
            
        }
    }
}