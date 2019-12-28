using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum GameEventType
{
    OnGameTick,
    OnServerTick,
    OnLateTick,

    OnPlayerLogin,
    OnPlayerChangeMap,
    OnPlayerDisconnect,
    OnPlayerInteract,
}

public class EventArgs
{
    public GameEventType type;
    public List<object> args;
    public int argInt;

    public EventArgs(GameEventType type, params object[] args)
    {
        this.type = type;
        this.args = new List<object>();
        this.args.AddRange(args);
    }
}

public static class EventHandler
{
    public delegate void _OnPlayerLogin(EventArgs args);
    public delegate void _OnPlayerChangeMap(EventArgs args);
    public delegate void _OnPlayerDisconnect(EventArgs args);
    public delegate void _OnPlayerInteract(EventArgs args);
    public delegate void _OnGameTick(EventArgs args);
    public delegate void _OnServerTick(EventArgs args);
    public delegate void _OnLateTick(EventArgs args);

    public static event _OnPlayerLogin OnPlayerLogin;
    public static event _OnPlayerChangeMap OnPlayerChangeMap;
    public static event _OnPlayerDisconnect OnPlayerDisconnect;
    public static event _OnPlayerInteract OnPlayerInteract;
    public static event _OnGameTick OnGameTick;
    public static event _OnServerTick OnServerTick;
    public static event _OnLateTick OnLateTick;

    public static void InvokeEvent(GameEventType type, params object[] args)
    {
        EventArgs eventArgs = new EventArgs(type, args);
        InvokeEvent(eventArgs);
    }

    public static void InvokeEvent(EventArgs args)
    {
        switch (args.type)
        {
            case GameEventType.OnPlayerLogin:
                OnPlayerLogin?.Invoke(args);
                break;
            case GameEventType.OnPlayerChangeMap:
                OnPlayerChangeMap?.Invoke(args);
                break;
            case GameEventType.OnPlayerDisconnect:
                OnPlayerDisconnect?.Invoke(args);
                break;
            case GameEventType.OnPlayerInteract:
                OnPlayerInteract?.Invoke(args); 
                break;
            case GameEventType.OnGameTick:
                OnGameTick?.Invoke(args);
                break;
            case GameEventType.OnServerTick:
                OnServerTick?.Invoke(args);
                break;
            case GameEventType.OnLateTick:
                OnLateTick?.Invoke(args);
                break;
            default:
                break;
        }
    }
}