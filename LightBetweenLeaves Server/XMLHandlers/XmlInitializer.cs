using GameDefinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class XmlInitializer : Initializable
{
    public int priority => 0;

    public void Initialize()
    {
        Debug.LogWithTime(LogLevel.Verbose, "Initializing Data Folder");
        ServerData.Initialize();
        ItemDatabase.Initialized();
    }
}
