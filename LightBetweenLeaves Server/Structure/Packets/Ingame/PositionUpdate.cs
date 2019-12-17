using CharacterStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PositionUpdate : Packet
{
    public int accountID;
    public float x;
    public float y;
    public float z;

    public PositionUpdate (Player player)
    {
        accountID = player.id;
        x = player.targetPosition.x;
        y = player.targetPosition.y;
        z = player.targetPosition.z;
    }

    public override void Serialize()
    {
        BeginWrite();
        writer.Write(accountID);
        writer.Write(x);
        writer.Write(y);
        writer.Write(z);
        EndWrite();
    }
    public override void Deserialize(BinaryReader reader)
    {
        accountID = reader.ReadInt32();
        x = reader.ReadSingle();
        y = reader.ReadSingle();
        z = reader.ReadSingle();
    }
}

