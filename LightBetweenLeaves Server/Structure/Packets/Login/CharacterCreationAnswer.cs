using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CharacterErrorCode
{
    Nothing,
    NameTaken,
    NameTooLong,
    ProfaneName,
    Other
}

public class CharacterCreationAnswer : Packet
{
    public CharacterErrorCode errorCode;
    public CharacterCreationAnswer() { type = PacketType.CharacterCreationAnswer; }
    public CharacterCreationAnswer(CharacterErrorCode _errorCode)
    {
        type = PacketType.CharacterCreationAnswer;
        errorCode = _errorCode;
    }

    public override void Serialize()
    {
        BeginWrite();
        writer.Write((int)errorCode);
        EndWrite();
    }
}
