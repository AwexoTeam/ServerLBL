using CharacterStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StatUpdatePacket : Packet
{
    public float hp;
    public float mp;
    public float sp;
    public float wound;
    public float exhaustion;
    public float hunger;

    public int strength;
    public int intellegence;
    public int dexterity;
    public int willpower;
    public int luck;

    public int level;
    public float exp;
    public int totalLevel;

    public int rebirthAmount;
    public int rebirthAge;
    public string lastRebirth;

    public StatUpdatePacket(Account account)
    {
        hp = account.hp;
        mp = account.mp;
        sp = account.sp;

        wound = account.wound;
        exhaustion = account.exhaustion;
        hunger = account.hunger;

        strength = account.strength;
        intellegence = account.intellegence;
        dexterity = account.dexterity;
        willpower = account.willpower;
        luck = account.luck;

        exp = account.exp;
        level = account.level;
        totalLevel = account.totalLevel;

        rebirthAmount = account.rebirthAmount;
        rebirthAge = account.rebirthAge;
        lastRebirth = account.lastRebirth;
    }

    public override void Serialize()
    {
        BeginWrite();
        writer.Write(hp);
        writer.Write(mp);
        writer.Write(sp);
        writer.Write(wound);
        writer.Write(exhaustion);
        writer.Write(hunger);
        writer.Write(strength);
        writer.Write(intellegence);
        writer.Write(dexterity);
        writer.Write(willpower);
        writer.Write(luck);
        writer.Write(level);
        writer.Write(exp);
        writer.Write(totalLevel);
        writer.Write(rebirthAmount);
        writer.Write(rebirthAge);
        writer.Write(lastRebirth);
        EndWrite();
    }

    public override void Deserialize(BinaryReader reader) { }
}
