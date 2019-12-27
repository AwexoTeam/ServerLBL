using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerMovementUpdate : Packet
{
    public int characterID;
    public float x;
    public float y;
    public float z;

    public PlayerMovementUpdate() { type = PacketType.PlayerMovementUpdate; }

    public override void Serialize()
    {
        BeginWrite();
        writer.Write(characterID);
        writer.Write(x);
        writer.Write(y);
        writer.Write(z);
        EndWrite();
    }
}
