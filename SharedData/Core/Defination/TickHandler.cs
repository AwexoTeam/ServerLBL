using GameDefinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

public class TickHandler : Initializable
{
    public static int GameTickInterval = 1000;
    public static int ServerTickInterval = 1000;
    public static int LateTickDelay = 1000;

    private static Timer GameTick;
    private static Timer ServerTick;
    private static Timer LateTick;

    public int priority => 2;

    public void Initialize()
    {
        GameTick = new Timer(GameTickInterval);
        ServerTick = new Timer(ServerTickInterval);
        LateTick = new Timer(ServerTickInterval + LateTickDelay);
        
        GameTick.AutoReset = true;
        ServerTick.AutoReset = true;
        LateTick.AutoReset = true;

        GameTick.Elapsed += OnGameTick;
        ServerTick.Elapsed += OnServerTick;
        LateTick.Elapsed += OnLateTick;

        GameTick.Start();
        ServerTick.Start();
        LateTick.Start();
    }

    private static void OnLateTick(object sender, ElapsedEventArgs e)
    {
        EventHandler.InvokeEvent(GameEventType.OnLateTick);
    }

    private static void OnServerTick(object sender, ElapsedEventArgs e)
    {
        EventHandler.InvokeEvent(GameEventType.OnServerTick);
    }

    private static void OnGameTick(object sender, ElapsedEventArgs e)
    {
        EventHandler.InvokeEvent(GameEventType.OnGameTick);
    }

}
