using System.Collections.Generic;
using System.Linq;
using Discord.WebSocket;

namespace RisuBump.DataManagement
{
    public class ManageRegisteredGuilds
    {
        Type _dbType = Dictionary<ulong, DataForms.GuildStorage>;
        private _dbType _savedGuilds;

        private const string _directory = "../DataStore";
        private const string _file = "../DataStore/registered_guilds.json";

        static ManageRegisteredGuilds()
        {
            _savedGuilds = DataStorage<_dbType>.GetAllDataFromFile(_file);
        }

        public static void AddGuildById(ulong id)
        {
            _savedGuilds.Add(id, new DataForms.GuildStorage());
            DataStorage<_dbType>.SaveDataToFile(_file, _savedGuilds);
        }

        public static void AddGuildById(ulong id, DataForms.GuildStorage guildData)
        {
            _savedGuilds.Add(id, guildData);
            DataStorage<_dbType>.SaveDataToFile(_file, _savedGuilds);
        }

        public static void RemoveGuildById(ulong id)
        {
            _savedGuilds.Remove(id);
            DataStorage<_dbType>.SaveDataToFile(_file, _savedGuilds);
        }

        public static void CleanGuilds(DiscordSocketClient client)
        {
            var guilds = client.Guilds;
            var updatedGuildList = new _dbType();

            foreach (KetValuePair<ulong, DataForms.GuildStorage> _savedGuild in _savedGuilds)
            {
                foreach (SocketGuild guild in guilds) updatedGuildList.Add(guild.Id, _savedGuild.Item2);
            }

            DataStorage<_dbType>.SaveDataToFile(_file, updatedGuildList);
        }
    }
}