using GameDefinations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

public static class ItemDatabase
{
    public static List<ItemData> itemDatabase;

    public static void Initialized()
    {
        itemDatabase = new List<ItemData>();
        
        XmlReader reader = XmlReader.Create("Data/DB/ItemDB.xml");
        
    }
}
