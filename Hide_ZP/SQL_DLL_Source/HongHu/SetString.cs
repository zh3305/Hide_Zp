namespace HongHu
{
    using Newtonsoft.Json;
    using System;

    public static class SetString
    {
        public static string ConfigJson = "";
        public static readonly string ConfigPath = (AppDomain.CurrentDomain.BaseDirectory + "config");
        public static string DBName = "";
        public static string DBPass = "";
        public static string DBServer = "";
        public static string DBUser = "";
        public static SQLLinkMode LinkMode = SQLLinkMode.SQL;
        public static string SQLConn = "";
        public static readonly string SqliteConn = @"Data Source=Data\Data.db3";

        public static T GetConfigJson<T>()
        {
            if (((ConfigJson == "null") || (ConfigJson == null)) || (ConfigJson == ""))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(ConfigJson);
        }

        public static void SetConJson(object obj)
        {
            ConfigJson = JsonConvert.SerializeObject(obj);
        }

        public enum SQLLinkMode
        {
            SQL = 1,
            SSPI = 3,
            WIN = 2
        }
    }
}

