using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterCreationAnswer : Packet
{
    public bool canCreate;
    public int errorCode;

    public CharacterCreationAnswer() { type = PacketType.CharacterCreationAnswer; }

    public override void Serialize()
    {
        BeginWrite();
        writer.Write(canCreate);
        writer.Write(errorCode);
        EndWrite();
    }

    public override void Deserialize(BinaryReader reader) { }
}
