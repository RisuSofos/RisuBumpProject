using System.IO;
using Newtonsoft.Json;

namespace RisuBump.DataManagement {
    public class DataStorage<T> {
        public static void SaveDataToFile(string filepath, T data) {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

        public static T GetAllDataFromFile(string filepath) {
            if (!File.Exists) {
                File.Create(filepath);
                return new T();
            }
            
            JsonConvert.DeserializeObject<T>(File.ReadAllText(filepath));
        }

    }

    public class DataStorage {
        public static bool FileExists(string filepath) => File.Exists();
        public static bool DirExists(string directory) => Directory.Exists();
    }
}