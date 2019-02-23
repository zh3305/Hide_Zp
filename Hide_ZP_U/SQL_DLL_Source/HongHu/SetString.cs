namespace HongHu
{
    using Newtonsoft.Json;
    using System;

    public static class SetString
    {
        public static string ConfigJson = "";
        public static readonly string ConfigPath = (AppDomain.CurrentDomain.BaseDirectory + "❤");
        public static string DBName = "";
        public static string DBPass = "";
        public static string DBServer = "";
        public static string DBUser = "";
        public static SQLLinkMode LinkMode = SQLLinkMode.SQL;
        public static string SQLConn = "Data Source=hong;Initial catalog=master;Integrated Security=False;User ID=sa;Password=;";
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
            /// <summary>
            /// sql认证
            /// </summary>
            SQL = 1,
            /// <summary>
            /// WINDOWS NT 集成安全认证
            /// </summary>
            SSPI = 3,
            /// <summary>
            /// WINDOWS 认证
            /// </summary>
            WIN = 2
        }
    }
}

