
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
    public static Dictionary<int, string> guidIDs;
    
    static void Main(string[] args)
    {
        StartServer();
        MemoryStream stream = new MemoryStream();
        BinaryReader reader = new BinaryReader(stream);

        Database.Initialize();
        Account a = new Account();
        a.Initialize(2);
        Debug.Log(a.name);

        guidIDs = new Dictionary<int, string>();
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
                            Debug.LogWithTime(LogLevel.Debug, "=>" + type + " -" + msg.connectionId);

                        string gID = reader.ReadString();

                        MessageHandler.HandleMessage(type, gID, reader, msg);
                        break;
                    case EventType.Disconnected:
                        Console.WriteLine(msg.connectionId + " Disconnected");
                        int id = msg.connectionId;
                        string guid = guidIDs[id];

                        guidIDs.Remove(id);
                        Player disconnectedPlayer = PlayerHandler.players.Find(p => p.guid == guid);
                        PlayerHandler.players.Remove(disconnectedPlayer);

                        Packet disconnection = new Packet("SERVER", PacketType.PlayerDisconnected);
                        disconnection.BeginWrite();
                        disconnection.writer.Write(guid);
                        disconnection.EndWrite();

                        SendAll(disconnection);
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
        Debug.LogWithTime(LogLevel.Debug, "<=" + packet.type + " -" + sendTo);
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