using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MovementRequest : Packet
{
    public float x;
    public float y;
    public float z;

    public override void Serialize() { }

    public override void Deserialize(BinaryReader reader)
    {
        x = reader.ReadSingle();
        y = reader.ReadSingle();
        z = reader.ReadSingle();
    }
}
