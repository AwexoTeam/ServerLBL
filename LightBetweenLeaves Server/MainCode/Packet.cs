using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PacketType
{
    Unknown,
    LoginRequest,
    LoginAnswer,
    MovementRequest,
    PositionUpdate,
    PlayerSyncRequest,
    PlayerDisconnected,
}

public class Packet
{
    public string guid;
    public PacketType type;
    public byte[] buffer;
    public BinaryWriter writer;

    public Packet(string _guid, PacketType _type)
    {
        guid = _guid;
        type = _type;
    }

    private MemoryStream stream;

    public void BeginWrite()
    {
        stream = new MemoryStream();
        writer = new BinaryWriter(stream);

        writer.Write((int)type);
        writer.Write(guid);
    }
    public void EndWrite()
    {
        buffer = stream.ToArray();
        stream.Close();
    }

}
