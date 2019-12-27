
using CharacterStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Telepathy;

public static class MainServer
{
    public static Server server = new Server();
    public static Dictionary<int, int> connectionToAccountID;
    
    static void Main(string[] args)
    {
        Telepathy.Logger.Log = Debug.TelepathyLog;
        Telepathy.Logger.LogWarning = Debug.TelepathyLogWarning;
        Telepathy.Logger.LogError = Debug.TelepathyError;

        XmlInitializer.Initialize();

        StartServer();
        MemoryStream stream = new MemoryStream();
        BinaryReader reader = new BinaryReader(stream);

        connectionToAccountID = new Dictionary<int, int>();
        
        Database.Initialize();
        TickHandler.Initialize();
        PlayerHandler.Initialize();

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
                        int id = reader.ReadInt32();

                        Packet packet = new Packet();

                        switch (type)
                        {
                            case PacketType.LoginRequest:
                                packet = new LoginRequest();
                                break;

                            case PacketType.CharacterCreationRequest:
                                packet = new CharacterCreationRequest();
                                break;

                            case PacketType.PlayerSyncRequest:
                                packet = new PlayerSyncRequest();
                                break;

                            case PacketType.PlayerMovementRequest:
                                packet = new PlayerMovementRequest();
                                break;

                            default:
                                break;
                        }

                        packet.Deserialize(reader);
                        packet.OnRecieve(msg);

                        if(type != PacketType.PlayerMovementRequest)
                            Debug.LogWithTime(LogLevel.Debug, "R: " + type + ", ID:" + msg.connectionId);

                        break;
                    case EventType.Disconnected:
                        
                        break;
                }
            }
        }
    }

    static void StartServer()
    {
        server.Start(ServerData.port);
    }

    public static void Send(int sendTo, Packet packet)
    {
        if (packet.type != PacketType.PlayerMovementUpdate)
        {
            Debug.LogWithTime(LogLevel.Debug, "S: " + packet.type + " ID:" + sendTo);
        }

        server.Send(sendTo, packet.buffer);
    }

    public static void SendAll(Packet packet)
    {
        foreach (var ids in connectionToAccountID)
        {
            Send(ids.Key, packet);
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

    public static int GetAccountIDByConnection(int connectionID)
    {
        return connectionToAccountID.First(x => x.Key == connectionID).Value;
    }
}