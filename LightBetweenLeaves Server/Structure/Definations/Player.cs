using CharacterStructures;
using GameDefinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Player
{
    public Account accountData;
    public Vector3 targetPosition;

    public int id { get { return accountData.id; } }

    public Player(Account account)
    {
        accountData = account;
    }
}

