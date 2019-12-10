using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LoginRequest : Packet
{
    public string username;
    public string password;

    public override void Serialize() { }
    public override void Deserialize(BinaryReader reader)
    {
        username = reader.ReadString();
        password = reader.ReadString();
    }
}
