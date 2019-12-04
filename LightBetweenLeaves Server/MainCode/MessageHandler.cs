
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
                case PacketType.MovementRequest:
                    HandleMovementUpdate(reader);
                    break;
                case PacketType.PositionUpdate:
                    HandlePositionUpdate(guid, reader);
                    break;
                default:
                    break;
            }
        }
    }

    public static void HandleLogin(BinaryReader reader, int connectionID)
    {
        bool canLogin = !MainServer.guidIDs.ContainsKey(connectionID);
        string uid = Guid.NewGuid().ToString();

        Packet packet = new Packet("SERVER", PacketType.LoginAnswer);
        packet.BeginWrite();
        packet.writer.Write(canLogin);
        packet.writer.Write(uid);
        packet.EndWrite();

        MainServer.Send(connectionID, packet);

        if (canLogin)
        {
            MainServer.guidIDs.Add(connectionID, uid);
            PlayerHandler.RegisterPlayer(uid);
            MainServer.SendAll(PlayerHandler.GetAllPlayerPositions());
        }
    }

    public static void HandleMovementUpdate(BinaryReader reader)
    {
        string guid = reader.ReadString();
        float x = reader.ReadSingle();
        float y = reader.ReadSingle();
        float z = reader.ReadSingle();

        PlayerHandler.MovementRequest(guid, x, y, z);
    }

    public static void HandleDisconnectedPlayer(string uid, int connectionID)
    {
        Packet playerConnected = new Packet("Server", PacketType.PlayerDisconnected);
        playerConnected.BeginWrite();
        playerConnected.writer.Write(uid);
        playerConnected.EndWrite();

        MainServer.SendAllExpect(connectionID, playerConnected);
    }

    public static void HandlePositionUpdate(string guid, BinaryReader reader)
    {
        float x = reader.ReadSingle();
        float y = reader.ReadSingle();
        float z = reader.ReadSingle();

        PlayerHandler.UpdatePosition(guid, x, y, z);
    }
}
