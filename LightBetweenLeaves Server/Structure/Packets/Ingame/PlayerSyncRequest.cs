using CharacterStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public class PlayerSyncRequest : Packet
{
    public PlayerSyncRequest() { type = PacketType.PlayerSyncRequest; }

    public override void OnRecieve(Message msg)
    {
        int accountID = MainServer.connectionToAccountID.First(x => x.Key == msg.connectionId).Value;
        Account account = new Account();
        account.Initialize(accountID);

        PlayerHandler.PlayerConnected(account);

        //TODO: send out our players.
        for (int i = 0; i < PlayerHandler.onlinePlayers.Count; i++)
        {
            Player currPlayer = PlayerHandler.onlinePlayers[i];

            Account playerAcc = new Account();
            UmaData data = new UmaData();
            
            playerAcc.Initialize(currPlayer.id);
            data.Initialize(currPlayer.id);
            
            PlayerSyncAnswer playerSync = new PlayerSyncAnswer
            {
                name = playerAcc.name,
                genativPronoun = data.genativPronoun,
                referalPronoun = data.referalPronoun,

                bodyType = data.bodyType,

                height = data.height,
                weight = data.weight,

                targetX = currPlayer.targetPosition.x,
                targetY = currPlayer.targetPosition.y,
                targetZ = currPlayer.targetPosition.z,

                characterID = currPlayer.id
            };

            playerSync.Serialize();
            playerSync.Send(msg.connectionId);
        }
    }
}
