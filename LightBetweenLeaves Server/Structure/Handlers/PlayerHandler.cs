using CharacterStructures;
using GameDefinations;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PlayerHandler
{
    public static List<Player> allPlayers;
    public static List<Player> onlinePlayers;
    
    //The time it takes if left unattented hunger reaches maxHungerDebuff in mms
    //End goal is 3600000 but will probably be lowered for testing.
    public static int hungerMaxTime = 30;
    public static float maxHungerDebuff = 30;
    public static float hungerRate
    {
        get
        {
            int amountOfTicks = hungerMaxTime / TickHandler.GameTickInterval;
            return (float)Math.Round(maxHungerDebuff / amountOfTicks);
        }
    }

    public static void Initialize()
    {
        allPlayers = new List<Player>();
        onlinePlayers = new List<Player>();

        string cmdStr = "SELECT * FROM Account";
        List<int> ids = new List<int>();
        MySqlDataReader reader = Database.ExecuteReader(cmdStr);

        while (reader.Read())
        {
            int id = (int)reader["id"];
            ids.Add(id);
        }
        reader.Close();
        reader.Dispose();

        for (int i = 0; i < ids.Count; i++)
        {
            Account account = new Account();
            account.Initialize(ids[i]);
            allPlayers.Add(new Player(account));
        }
        
        EventHandler.OnGameTick += PlayerGameTick;
        EventHandler.OnLateTick += LateTickSavePlayers;
        EventHandler.OnServerTick += ServerTickAccountUpdate;

        Debug.LogWithTime(LogLevel.Verbose, "Player Database Initialized");
    }

    public static void SendPositions()
    {
        for (int i = 0; i < onlinePlayers.Count; i++)
        {
            Player player = onlinePlayers[i];
            
            PlayerMovementUpdate movementUpdate = new PlayerMovementUpdate
            {
                characterID = player.id,
                x = player.targetPosition.x,
                y = player.targetPosition.y,
                z = player.targetPosition.z,
            };


            movementUpdate.Serialize();
            MainServer.SendAll(movementUpdate);
        }
    }

    public static void PlayerConnected(Account loggedInAccount)
    {
        Player newPlayer = new Player(loggedInAccount);
        onlinePlayers.Add(newPlayer);
    }

    private static void PlayerGameTick(EventArgs args)
    {
        for (int i = 0; i < onlinePlayers.Count; i++)
        {
            Player player = onlinePlayers[i];

            player.accountData.hunger += hungerRate;
            //TODO: handle equipment durability loss here too :D
            //TODO: Send Stat Update to players.
        }
    }
    private static void ServerTickAccountUpdate(EventArgs args)
    {
        
    }
    private static void LateTickSavePlayers(EventArgs args)
    {
        for (int i = 0; i < onlinePlayers.Count; i++)
        {
            Account acc = onlinePlayers[i].accountData;
            acc.Update(acc.id);
        }
    }
    
    public static int GetAccountIdByConID(int connectionID)
    {
        if (MainServer.connectionToAccountID.ContainsKey(connectionID))
        {
            return MainServer.connectionToAccountID[connectionID];
        }
        else return -1;
    }
    public static Account GetAccountByConnectionID(int connectionID)
    {
        int id = GetAccountIdByConID(connectionID);
        if(id > -1)
        {
            return GetAccountByID(id);
        }
        else { return null; }
    }
    public static Account GetAccountByID(int id)
    {
        Account acc = new Account();
        acc.Initialize(id);

        return acc;
    }
}
