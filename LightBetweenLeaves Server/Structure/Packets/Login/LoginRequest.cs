using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public class LoginRequest : Packet
{
    public string username;
    public string password;

    public LoginRequest() { type = PacketType.LoginRequest; }

    public override void Serialize() { }
    public override void Deserialize(BinaryReader reader)
    {
        username = reader.ReadString();
        password = reader.ReadString();
    }

    public override void OnRecieve(Message msg)
    {
        //TODO: reimplement character check logic.
        
        LoginAnswer answer = DatabaseHandler.DoLoginCheck(msg.connectionId, username, password);
        MainServer.connectionToAccountID.Add(msg.connectionId, answer.characterID);

        answer.Serialize();
        MainServer.Send(msg.connectionId, answer);;
    }
}
