using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

public static class ServerData
{
    public static int port;
    public static int maxPlayers;
    public static bool useChatFilter;
    public static bool useNameFilter;
    public static int miniumAge;

    public static float apMultiplier;
    public static float dropMultiplier;
    public static float goldMultiplier;
    public static float questExpMultiplier;
    public static float monsterExpMultiplier;
    public static float productionExpMultiplier;
    public static float skillExpMultiplier;

    public static void Initialize()
    {
        XmlReader reader = XmlReader.Create("Data/Options/ServerData.xml");

        while (!reader.EOF)
        {
            if(reader.Name != "ServerData") { reader.ReadToFollowing("ServerData"); }
            if (!reader.EOF)
            {
                XElement serverData = (XElement)XElement.ReadFrom(reader);

                DatabaseHandler.server = (string)serverData.Element("Server");
                DatabaseHandler.username = (string)serverData.Element("Username");
                DatabaseHandler.password = (string)serverData.Element("Password");
                DatabaseHandler.database = (string)serverData.Element("Database");

                TickHandler.GameTickInterval = (int)serverData.Element("GameTick");
                TickHandler.ServerTickInterval = (int)serverData.Element("ServerTick");
                TickHandler.LateTickDelay = (int)serverData.Element("LateTickDelay");

                port = (int)serverData.Element("Port");
                maxPlayers = (int)serverData.Element("MaxPlayers");
                useChatFilter = (Boolean)serverData.Element("ChatFilter");
                useNameFilter  = (Boolean)serverData.Element("NameFilter");
                miniumAge = (int)serverData.Element("AgeRequirment");

                apMultiplier = (float)serverData.Element("AP");
                dropMultiplier = (float)serverData.Element("DROP");
                goldMultiplier = (float)serverData.Element("GOLD_DROP");
                questExpMultiplier = (float)serverData.Element("QUEST_EXP");
                monsterExpMultiplier = (float)serverData.Element("MONSTER_EXP");
                productionExpMultiplier = (float)serverData.Element("PRODUCTION_EXP");
                skillExpMultiplier = (float)serverData.Element("SKILL_EXP");

                PlayerHandler.hungerMaxTime = (int)serverData.Element("HungerMaxTime");
                PlayerHandler.maxHungerDebuff = (float)serverData.Element("MaxHungerDebuff");
            }
        }

        Debug.LogWithTime(LogLevel.Verbose, "Server Data loaded");
    }
}