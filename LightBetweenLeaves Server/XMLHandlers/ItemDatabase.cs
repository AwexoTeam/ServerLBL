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

        while (!reader.EOF)
        {
            if (reader.Name != "ItemDB") { reader.ReadToFollowing("ItemDB"); }
            if (!reader.EOF)
            {
                XElement itemDatabase = (XElement)XElement.ReadFrom(reader);
                Console.Write(itemDatabase.Attribute("id") + ", ");
                Console.Write(itemDatabase.Attribute("name") + ", ");
                Console.Write(itemDatabase.Attribute("description") + ", ");
                Console.Write(itemDatabase.Attribute("usableID") + ", ");
                Console.Write("\n");
            }
        }
    }
}
