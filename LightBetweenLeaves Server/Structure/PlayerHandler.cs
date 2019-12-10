using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterStructures;
using DataTypeDefinations;

public class Player
{
    public Account account;
    public Vector3 targetPosition;
    public Vector3 currentPosition
    {
        get { return new Vector3(account.x, account.y, account.z); }
        set
        {
            account.x = value.x;
            account.y = value.y;
            account.z = value.z;
        }
    }

    public Player(int id)
    {
        account.Initialize(id);
        targetPosition = new Vector3(0, 0, 0);
    }
}

public static class PlayerHandler
{

}
