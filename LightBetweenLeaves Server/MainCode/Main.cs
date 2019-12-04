
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;
using ZeroFormatter;

public static class MainServer
{
    public static Server server = new Server();
    public static Dictionary<int, string> guidIDs;

    public static bool oof = true;

    static void Main(string[] args)
    {
        StartServer();
        MemoryStream stream = new MemoryStream();
        BinaryReader reader = new BinaryReader(stream);
        
        guidIDs = new Dictionary<int, string>();
        for (; ; )
        {
            if (oof && server.Active)
            {
                Debug.LogWithTime(false, LogLevel.Verbose, "Hey");
                Debug.LogWithBacktrack(false, LogLevel.Debug, "Oi");
                Debug.LogWithBacktrack(true, LogLevel.Debug, "Error");

                oof = false;
            }

            Telepathy.Message msg;
            while (server.GetNextMessage(out msg))
            {
                switch (msg.eventType)
                {
                    case Telepathy.EventType.Connected:
                        Console.WriteLine(msg.connectionId + " Connected");

                        break;
                    case Telepathy.EventType.Data:
                        stream = new MemoryStream(msg.data);
                        reader = new BinaryReader(stream);

                        PacketType type = (PacketType)reader.ReadInt32();
                        string gID = reader.ReadString();

                        MessageHandler.HandleMessage(type, gID, reader, msg);
                        break;
                    case Telepathy.EventType.Disconnected:
                        Console.WriteLine(msg.connectionId + " Disconnected");
                        
                        break;
                }
            }
        }
    }

    static void StartServer()
    {
        server.Start(1337);
    }

    public static void Send(int sendTo, Packet packet)
    {
        server.Send(sendTo, packet.buffer);
    }

    public static void SendAll(Packet packet)
    {
        foreach (var guidIDPair in guidIDs)
        {
            Send(guidIDPair.Key, packet);
        }
    }

    public static void SendAllExpect(int avoid, Packet packet)
    {
        foreach (var guidIDPair in guidIDs)
        {
            if(guidIDPair.Key != avoid)
                Send(guidIDPair.Key, packet);
        }
    }
}