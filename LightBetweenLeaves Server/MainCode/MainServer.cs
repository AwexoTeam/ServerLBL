
using CharacterStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Telepathy;

public static partial class MainServer
{
    public static Server server = new Server();
    public static Dictionary<int, int> connectionToAccountID;
    
    private static void StartServer()
    {
        server.Start(ServerData.port);
    }
    private static void StopServer() { }

    private static void ConnectionHandler(Message msg) { }
    private static void DisconnectionHandler(Message msg) { }

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
}