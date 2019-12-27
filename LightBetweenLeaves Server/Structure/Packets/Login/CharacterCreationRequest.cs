using CharacterStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public class CharacterCreationRequest : Packet
{
    public string name;
    public string genativPronoun;
    public string referalPronoun;

    public int bodyType;

    public float height;
    public float weight;

    public CharacterCreationRequest()
    {
        type = PacketType.CharacterCreationRequest;
    }

    public override void Deserialize(BinaryReader reader)
    {
        name = reader.ReadString();
        genativPronoun = reader.ReadString();
        referalPronoun = reader.ReadString();

        bodyType = reader.ReadInt32();

        height = reader.ReadSingle();
        weight = reader.ReadSingle();
    }

    public override void OnRecieve(Message msg)
    {
        //TODO: do all checks on name etc.
        CharacterCreationAnswer answer = new CharacterCreationAnswer(CharacterErrorCode.Nothing);
        int accountID = MainServer.connectionToAccountID.First(x => x.Key == msg.connectionId).Value;

        Account account = new Account();
        UmaData data = new UmaData();

        account.Initialize(accountID);
        data.Initialize(accountID);

        account.name = name;

        data.height = height;
        data.weight = weight;
        data.bodyType = bodyType;

        data.referalPronoun = referalPronoun;
        data.genativPronoun = genativPronoun;

        data.ownerID = accountID;

        account.Update(accountID);
        data.Insert();
        
        answer.Serialize();
        MainServer.Send(msg.connectionId, answer);
    }
}