using System.IO;

namespace SASS_Splitter {
        public static class SCSSFileSource {
            public static StreamReader GetInputStream(string fileName) {
                var oldFile = fileName + ".old";
                if (!File.Exists(oldFile))
                    File.Copy(fileName, oldFile);
                var inputStream = new StreamReader(oldFile);
                return inputStream;
            }
        }
}