using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterCreationRequest : Packet
{
    public string name;
    public string pronouns;
    public int bodyType;

    public float height;
    public float weight;
    
    public override void Serialize() { }
    public override void Deserialize(BinaryReader reader)
    {
        name = reader.ReadString();
        pronouns = reader.ReadString();
        bodyType = reader.ReadInt32();
        height = reader.ReadSingle();
        weight = reader.ReadSingle();
    }
}
