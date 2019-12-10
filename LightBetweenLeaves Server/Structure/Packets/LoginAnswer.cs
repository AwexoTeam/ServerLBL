using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LoginAnswer : Packet
{
    public bool canLogin;
    public bool hasCharacter;
    public int errorCode;
    public int characterID;

    public LoginAnswer() { type = PacketType.LoginAnswer; }

    public override void Serialize()
    {
        BeginWrite();
        writer.Write(canLogin);
        writer.Write(hasCharacter);
        writer.Write(errorCode);
        writer.Write(characterID);
        EndWrite();
    }
    public override void Deserialize(BinaryReader reader) { }
}
