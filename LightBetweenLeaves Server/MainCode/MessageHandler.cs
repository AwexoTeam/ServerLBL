
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public static class MessageHandler
{
    public static void HandleMessage(PacketType type, int id, BinaryReader reader, Message msg)
    {
        if (type == PacketType.LoginRequest){ LoginRequest(msg.connectionId, reader); }
        else
        {
            switch (type)
            {
                case PacketType.Unknown:
                    break;
                case PacketType.CharacterCreationRequest:
                    RequestCharacterCreation(reader, msg);
                    break;
                case PacketType.MovementRequest:
                    break;
                case PacketType.PlayerSyncRequest:
                    break;
                case PacketType.PlayerDisconnected:
                    break;
                default:
                    break;
            }
        }
    }

    private static void RequestCharacterCreation(BinaryReader reader, Message msg)
    {
        CharacterCreationRequest request = new CharacterCreationRequest();
        request.Deserialize(reader);

        CharacterCreationAnswer answer = new CharacterCreationAnswer();

        if (Database.DoesCharacterNameExist(request.baseInfo.name))
        {
            answer.canCreate = true;
            answer.errorCode = 0;
        }
        else
        {
            answer.canCreate = false;

            //Character name taken
            answer.errorCode = 1;

            //TODO: check if height and such are good! Config.
        }

        answer.Serialize();
        answer.Send(msg.connectionId);
    }

    public static void LoginRequest(int connectionID, BinaryReader reader)
    {
        LoginRequest request = new LoginRequest();
        request.Deserialize(reader);

        Database.DoLoginCheck(connectionID, request);
    }

}
