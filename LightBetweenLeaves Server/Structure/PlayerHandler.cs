using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypeDefinations;

public class Player
{
    public string guid;
    public Vector3 targetPosition;
    public Vector3 currentPosition;

    public Player(string guid)
    {
        this.guid = guid;
        targetPosition = new Vector3(0, 0, 0);
        currentPosition = new Vector3(0, 0, 0);
    }
}

public static class PlayerHandler
{
    public static List<Player> players = new List<Player>();

    public static void RegisterPlayer(string guid)
    {
        players.Add(new Player(guid));
    }

    public static void MovementRequest(string guid, float x, float y, float z)
    {
        Player player = players.Find(p => p.guid == guid);
        if(player != null)
        {
            player.targetPosition = new Vector3(x, y, z);

            Packet packet = new Packet("SERVER", PacketType.MovementRequest);
            packet.BeginWrite();
            packet.writer.Write(guid);
            packet.writer.Write(x);
            packet.writer.Write(y);
            packet.writer.Write(z);
            packet.EndWrite();

            MainServer.SendAll(packet);
        }
    }

    public static Packet GetAllPlayerPositions()
    {
        Packet packet = new Packet("SERVER", PacketType.PlayerSyncRequest);
        packet.BeginWrite();
        packet.writer.Write(players.Count);
        for (int i = 0; i < players.Count; i++)
        {
            Player currPlayer = players[i];
            packet.writer.Write(currPlayer.guid);
            packet.writer.Write(currPlayer.currentPosition.x);
            packet.writer.Write(currPlayer.currentPosition.y);
            packet.writer.Write(currPlayer.currentPosition.z);
            
            packet.writer.Write(currPlayer.targetPosition.x);
            packet.writer.Write(currPlayer.targetPosition.y);
            packet.writer.Write(currPlayer.targetPosition.z);
        }
        packet.EndWrite();
        return packet;
    }

    public static Packet GetPlayerPosition(string guid)
    {
        Player currPlayer = players.Find(p => p.guid == guid);

        Packet packet = new Packet("SERVER", PacketType.PositionUpdate);
        packet.BeginWrite();
        packet.writer.Write(1);
        packet.writer.Write(currPlayer.guid);
        packet.writer.Write(currPlayer.currentPosition.x);
        packet.writer.Write(currPlayer.currentPosition.y);
        packet.writer.Write(currPlayer.currentPosition.z);

        packet.writer.Write(currPlayer.targetPosition.x);
        packet.writer.Write(currPlayer.targetPosition.y);
        packet.writer.Write(currPlayer.targetPosition.z);
        packet.EndWrite();

        return packet;
    }

    public static void UpdatePosition(string guid, float x, float y, float z)
    {
        players.Find(p => p.guid == guid).currentPosition = new Vector3(x, y, z);
    }
}
