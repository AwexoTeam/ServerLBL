
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public static class MessageHandler
{
    public static void HandleMessage(PacketType type, string guid, BinaryReader reader, Message msg)
    {
        if(type == PacketType.LoginRequest) { HandleLogin(reader, msg.connectionId); }

        string realGUID = MainServer.guidIDs[msg.connectionId];
        if (realGUID == guid)
        {
            switch (type)
            {
                case PacketType.MovementUpdate:
                    HandleMovementUpdate(reader);
                    break;
                default:
                    break;
            }
        }
    }

    public static void HandleLogin(BinaryReader reader, int connectionID)
    {
        Packet packet = new Packet("SERVER", PacketType.LoginAnswer);
        packet.BeginWrite();

        if (!MainServer.guidIDs.ContainsKey(connectionID))
        {
            packet.writer.Write(true);
            string uid = Guid.NewGuid().ToString();
            packet.writer.Write(uid);
            MainServer.guidIDs.Add(connectionID, uid);

            foreach (var item in MainServer.guidIDs)
            {
                Packet p = new Packet("SERVER", PacketType.PlayerConnected);
                p.BeginWrite();
                p.writer.Write(item.Value);
                p.EndWrite();

                if (item.Key != connectionID)
                {
                    MainServer.Send(connectionID, p);
                }
            }

            HandleConnectedPlayer(uid, connectionID);
        }
        else { packet.writer.Write(false); }
        packet.EndWrite();

        MainServer.Send(connectionID, packet);

        
    }

    public static void HandleMovementUpdate(BinaryReader reader)
    {
        string guid = reader.ReadString();
        float x = reader.ReadSingle();
        float y = reader.ReadSingle();
        float z = reader.ReadSingle();

        Packet packet = new Packet("SERVER", PacketType.MovementUpdate);
        packet.BeginWrite();
        packet.writer.Write(guid);
        packet.writer.Write(x);
        packet.writer.Write(y);
        packet.writer.Write(z);
        packet.EndWrite();

        MainServer.SendAll(packet);
    }

    public static void HandleConnectedPlayer(string uid, int connectionID)
    {
        Packet playerConnected = new Packet("Server", PacketType.PlayerConnected);
        playerConnected.BeginWrite();
        playerConnected.writer.Write(uid);
        playerConnected.EndWrite();

        MainServer.SendAllExpect(connectionID, playerConnected);
        
    }

    public static void HandleDisconnectedPlayer(string uid, int connectionID)
    {
        Packet playerConnected = new Packet("Server", PacketType.PlayerDisconnected);
        playerConnected.BeginWrite();
        playerConnected.writer.Write(uid);
        playerConnected.EndWrite();

        MainServer.SendAllExpect(connectionID, playerConnected);
    }
}
