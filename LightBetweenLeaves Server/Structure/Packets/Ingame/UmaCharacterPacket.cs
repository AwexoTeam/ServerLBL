using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UmaCharacterPacket : Packet
{
    public string name;
    public string pronouns;
    public int bodyType;

    public float height;
    public float weight;

    public UmaCharacterPacket(string _name, string _pronouns, int _bodyType)
    {
        type = PacketType.UmaCharacterPacket;
        name = _name;
        pronouns = _pronouns;
        bodyType = _bodyType;
    }

    public override void Serialize()
    {
        BeginWrite();
        writer.Write(name);
        writer.Write(pronouns.ToLower());
        writer.Write(bodyType);
        writer.Write(height);
        writer.Write(weight);
    }

    public override void Deserialize(BinaryReader reader)
    {
        name = reader.ReadString();
        pronouns = reader.ReadString();
        bodyType = reader.ReadInt32();
        height = reader.ReadSingle();
        weight = reader.ReadSingle();
    }
}
