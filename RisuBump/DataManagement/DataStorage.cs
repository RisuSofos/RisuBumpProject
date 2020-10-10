using System.IO;
using Newtonsoft.Json;

namespace RisuBump.DataManagement {
    public class DataStorage<T> {
        public static void SaveDataToFile(string filepath, T data) {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

        public static T GetAllDataFromFile(string filepath) {
            if (!File.Exists(filepath)) File.Create(filepath);
            
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filepath));
        }

    }

    public class DataStorage {
        public static bool FileExists(string filepath) => File.Exists(filepath);
        public static bool DirExists(string directory) => Directory.Exists(directory);
    }
}