using GameDefinations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public class PlayerMovementRequest : Packet
{
    public float x;
    public float y;
    public float z;

    public PlayerMovementRequest() { type = PacketType.PlayerMovementRequest; }

    public override void Deserialize(BinaryReader reader)
    {
        x = reader.ReadSingle();
        y = reader.ReadSingle();
        z = reader.ReadSingle();
    }

    public override void OnRecieve(Message msg)
    {
        int accountID = MainServer.GetAccountIDByConnection(msg.connectionId);

        //TODO: check Collision map.
        int index = PlayerHandler.onlinePlayers.FindIndex(p => p.id == accountID);
        PlayerHandler.onlinePlayers[index].targetPosition = new Vector3(x, y, z);

        PlayerHandler.SendPositions();
    }
}
