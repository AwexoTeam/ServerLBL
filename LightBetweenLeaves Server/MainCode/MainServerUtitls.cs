using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class MainServer
{
    public static int GetAccountIDByConnection(int connectionID)
    {
        return connectionToAccountID.First(x => x.Key == connectionID).Value;
    }
}