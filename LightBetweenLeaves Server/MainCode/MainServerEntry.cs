﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public static partial class MainServer
{
    private static MemoryStream stream;
    private static BinaryReader reader;

    public static Dictionary<PacketType, Packet> packets;

    static void Main(string[] args)
    {
        Initialize();
        ModHandler.Initialize();
        ModHandler.LoadPackets();
        StartServer();

        MessageLoop();
    }

    public static void Initialize()
    {
        Telepathy.Logger.Log = Debug.TelepathyLog;
        Telepathy.Logger.LogWarning = Debug.TelepathyLogWarning;
        Telepathy.Logger.LogError = Debug.TelepathyError;
        
        stream = new MemoryStream();
        reader = new BinaryReader(stream);

        connectionToAccountID = new Dictionary<int, int>();
        packets = new Dictionary<PacketType, Packet>();
    }
    public static void MessageLoop()
    {
        for (; ; )
        {
            Message msg;
            while (server.GetNextMessage(out msg))
            {
                switch (msg.eventType)
                {
                    case EventType.Connected:
                        ConnectionHandler(msg);
                        break;
                    case EventType.Data:
                        ProcessPacket(msg);
                        break;
                    case EventType.Disconnected:
                        DisconnectionHandler(msg);
                        break;
                }
            }
        }
    }

    private static void ProcessPacket(Message msg)
    {
        stream = new MemoryStream(msg.data);
        reader = new BinaryReader(stream);

        PacketType type = (PacketType)reader.ReadInt32();
        int id = reader.ReadInt32();

        if (packets.ContainsKey(type))
        {
            Packet packet = packets[type];
            packet.Deserialize(reader);
            packet.OnRecieve(msg);

            if (type != PacketType.PlayerMovementRequest)
                Debug.LogWithTime(LogLevel.Debug, "R: " + type + ", ID:" + msg.connectionId);
        }
    }
}
