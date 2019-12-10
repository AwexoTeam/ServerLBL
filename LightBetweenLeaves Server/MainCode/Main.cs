
using CharacterStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public static class MainServer
{
    public static Server server = new Server();
    public static Dictionary<int, int> connectionToAccountID;

    static void Main(string[] args)
    {
        StartServer();
        MemoryStream stream = new MemoryStream();
        BinaryReader reader = new BinaryReader(stream);

        connectionToAccountID = new Dictionary<int, int>();

        Database.Initialize();
        
        for (; ; )
        {
            Message msg;
            while (server.GetNextMessage(out msg))
            {
                switch (msg.eventType)
                {
                    case EventType.Connected:
                        
                        break;
                    case EventType.Data:
                        stream = new MemoryStream(msg.data);
                        reader = new BinaryReader(stream);
                        
                        PacketType type = (PacketType)reader.ReadInt32();
                        if(type != PacketType.PositionUpdate)
                            Debug.LogWithTime(LogLevel.Debug, "=>" + type + ", ID:" + msg.connectionId);

                        int id = reader.ReadInt32();
                        
                        MessageHandler.HandleMessage(type, id, reader, msg);
                        break;
                    case EventType.Disconnected:
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
        Debug.LogWithTime(LogLevel.Debug, "<=" + packet.type + " ID:" + sendTo);
        server.Send(sendTo, packet.buffer);
    }

    public static void SendAll(Packet packet)
    {
        foreach (var ids in connectionToAccountID)
        {
            Send(ids.Value, packet);
        }
    }

    public static void SendAllExpect(int avoid, Packet packet)
    {
            foreach (var ids in connectionToAccountID)
            {
                if (ids.Value != avoid)
                    Send(ids.Value, packet);
            }
    }
}