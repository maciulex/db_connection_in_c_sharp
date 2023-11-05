using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace FILES {
    class FILES {
        public string [] GetDirectories(string path) {
            return Directory.GetDirectories(path);
        }

        public string [] GetFiles(string path) {
            return Directory.GetFiles(path);
        }

        public dynamic? getJsonFile(string path) {
            var data  = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(path));
            // var data = JsonConvert.DeserializeObject<Dictionary<string, Dataset>>(File.ReadAllText(path));
            return data;
        }

        public bool directoryExists(string path) {
            if (Directory.Exists(path)){
                return true;
            }
            return false;
        }

        public bool fileExists(string path) {
            return File.Exists(path);
        }

        public bool createDirectory(string path) {
            if (directoryExists(path)) return false;
            Directory.CreateDirectory(path);

            return true;
        }
    }   
}