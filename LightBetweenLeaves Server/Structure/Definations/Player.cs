using CharacterStructures;
using GameDefinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player
{
    public int id { get { return accountData.id; } }
    public Account accountData;
    public Vector3 targetPosition;
    public Vector3 currPosition
    {
        get
        {
            return new Vector3(accountData.x, accountData.y, accountData.z);
        }
    }

    public Player(Account account)
    {
        accountData = account;
    }
}
