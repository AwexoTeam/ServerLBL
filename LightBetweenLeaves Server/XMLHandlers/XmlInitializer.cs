using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class XmlInitializer
{
    public static void Initialize()
    {
        Debug.LogWithTime(LogLevel.Verbose, "Initializing Data Folder");
        ServerData.Initialize();
        ItemDatabase.Initialized();

    }
}
