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
            MainServer.Send(msg.connectionId, playerSync);
        }

        //TODO: send the new players to everyone else.
        Player player = PlayerHandler.onlinePlayers.Find(p => p.id == accountID);
        UmaData umaData = new UmaData();
        umaData.Initialize(accountID);

        PlayerSyncAnswer newPlayerSync = new PlayerSyncAnswer
        {
            name = player.accountData.name,
            genativPronoun = umaData.genativPronoun,
            referalPronoun = umaData.referalPronoun,

            bodyType = umaData.bodyType,

            height = umaData.height,
            weight = umaData.weight,

            targetX = player.targetPosition.x,
            targetY = player.targetPosition.y,
            targetZ = player.targetPosition.z,

            characterID = player.id
        };

        newPlayerSync.Serialize();

        MainServer.SendAllExpect(msg.connectionId, newPlayerSync);
    }
}
