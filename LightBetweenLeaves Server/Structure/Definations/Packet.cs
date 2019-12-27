using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;

public enum PacketType
{
    Unknown,

    LoginRequest,
    CharacterCreationRequest,
    PlayerSyncRequest,
    PlayerMovementRequest,

    LoginAnswer,
    CharacterCreationAnswer,
    PlayerSyncAnswer,
    PlayerMovementUpdate,

    PlayerDisconnected,
}

public class Packet
{
    public int id = -1;
    public PacketType type;
    public byte[] buffer;
    public BinaryWriter writer;
    
    private MemoryStream stream;
    
    public Packet() { }
    public Packet(byte[] data)
    {
        buffer = data;
    }

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

    public virtual void Serialize() { }
    public virtual void Deserialize(BinaryReader reader) { }

    public virtual void OnRecieve(Message msg) { }

    public void Send(int id, bool isPlayerID = false)
    {
        MainServer.Send(id, this);
    }
    public void SendAll()
    {
        MainServer.SendAll(this);
    }
    public void SendAllExcept(int id, bool isPlayerID = false)
    {
        MainServer.SendAllExpect(id, this);
    }
    
}
