using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterCreationRequest : Packet
{
    public UmaCharacterPacket baseInfo;

    public CharacterCreationRequest() { type = PacketType.CharacterCreationRequest; }

    public override void Serialize() { }
    public override void Deserialize(BinaryReader reader)
    {
        UmaCharacterPacket packet = new UmaCharacterPacket("", "", 0);
        packet.Deserialize(reader);

        baseInfo = packet;
    }
}
