using System.IO;
using Newtonsoft.Json;

namespace RisuBump {
    public class Config {
        private const string file = "./bot/config.json";
        private const string dir = "./bot/";

        public static BotConfig Bot;
        static Config() {
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (!File.Exists(file)) {
                Bot = new BotConfig();

                string json = JsonConvert.SerializeObject(Bot, Formatting.Indented);
                File.WriteAllText(file, json);
            }
            else Bot = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(file));
        }

        public struct BotConfig {
            public string Token;
            public string DefaultPrefix;
        }
    }
}