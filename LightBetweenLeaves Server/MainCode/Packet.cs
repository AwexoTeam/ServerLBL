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
    CharacterCreationRequest,
    CharacterCreationAnswer,
    MovementRequest,
    PositionUpdate,
    PlayerSyncRequest,
    PlayerDisconnected,
    UmaCharacterPacket,
}

public abstract class Packet
{
    public int id = -1;
    public PacketType type;
    public byte[] buffer;
    public BinaryWriter writer;
    
    private MemoryStream stream;

    public void BeginWrite()
    {
        stream = new MemoryStream();
        writer = new BinaryWriter(stream);

        writer.Write((int)type);
        writer.Write(id);
    }
    public void EndWrite()
    {
        buffer = stream.ToArray();
        stream.Close();
    }

    public abstract void Serialize();
    public abstract void Deserialize(BinaryReader reader);

    public void Send(int id)
    {
        MainServer.Send(id, this);
    }
    public void SendAll()
    {
        MainServer.SendAll(this);
    }
    public void SendAllExcept(int id)
    {
        MainServer.SendAllExpect(id, this);
    }
}
