using DatabaseData;
using DataTypeDefinations;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace CharacterStructures
{
    public class Account : DatabaseTable
    {
        [NOT_NULL] public string name { get; set; }
        [NOT_NULL] public string username { get; set; }
        [NOT_NULL] public string password { get; set; }

        public int sceneID { get; set; }

        public string lastLogin { get; set; }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public int totalLevel { get; set; }
        public int xp { get; set; }
        public bool alive { get; set; }
        public int coin { get; set; }

        public float hp { get; set; }
        public float mp { get; set; }
        public float sp { get; set; }

        public float wound { get; set; }
        public float hunger { get; set; }
        public float exhaustion { get; set; }

        public string lastRebirth { get; set; }
        public int rebirthAge { get; set; }
        public int rebirthAmount { get; set; }

        public override void Initialize(int id)
        {
            StartReader(id);
            while (reader.Read())
            {
                this.name = (string)reader["name"];
                this.username = (string)reader["username"];
                this.password = (string)reader["password"];
                this.sceneID = (int)reader["sceneID"];
                this.lastLogin = (string)reader["lastLogin"];

                this.x = (float)reader["x"];
                this.y = (float)reader["y"];
                this.z = (float)reader["z"];

                this.totalLevel = (int)reader["totalLevel"];
                this.xp = (int)reader["xp"];
                this.alive = (bool)reader["alive"];
                this.coin = (int)reader["coin"];
                
                this.hp = (float)reader["hp"];
                this.mp = (float)reader["mp"];
                this.sp = (float)reader["sp"];
                
                this.wound = (float)reader["wound"];
                this.hunger = (float)reader["hunger"];
                this.exhaustion = (float)reader["exhaustion"];

                this.lastRebirth = (string)reader["lastRebirth"];
                this.rebirthAge = (int)reader["rebirthAge"];
                this.rebirthAmount = (int)reader["rebirthAmount"];
            }
            EndReader();
        }

        public override void Insert()
        {
            Database.Insert
           (
                this,
                name,
                username,
                password,
                sceneID,
                lastLogin,
                x,
                y,
                z,
                totalLevel,
                xp,
                alive,
                coin,
                hp,
                mp,
                sp,
                wound,
                hunger,
                exhaustion,
                lastRebirth,
                rebirthAge,
                rebirthAmount
           );
        }
        public override void Update(int packetID)
        {
            Database.Update
           (
                this,
                packetID,
                name,
                username,
                password,
                sceneID,
                lastLogin,
                x,
                y,
                z,
                totalLevel,
                xp,
                alive,
                coin,
                hp,
                mp,
                sp,
                wound,
                hunger,
                exhaustion,
                lastRebirth,
                rebirthAge,
                rebirthAmount
           );
        }
    }

    public class ItemEntry : DatabaseTable
    {
        #region Identifiers
        [NOT_NULL]public int ownerID { get; set; }
        [NOT_NULL]public int itemID { get; set; }
        public int storageID { get; set; }
        #endregion

        #region Base Info 
        public int amount { get; set; }
        public float profiency { get; set; }
        public float durability { get; set; }
        public bool isUnique { get; set; }
        #endregion

        #region Upgrade Info
        public int prefixID { get; set; }
        public int suffixID { get; set; }
        public int retuningID { get; set; }
        public int finalUpgradeType { get; set; }
        public int finalUpgradeStep { get; set; }
        #endregion

        #region Stats
        public int minDamage { get; set; }
        public int maxDamage { get; set; }

        public int minWound { get; set; }
        public int maxWound { get; set; }

        public int critical { get; set; }
        public int stability { get; set; }
        public int armorPierce { get; set; }

        public int protection { get; set; }
        public int defence { get; set; }
        public int magicDefence { get; set; }

        public int magicAffinity { get; set; }
        public int magicBalance { get; set; }
        public int chainCast { get; set; }
        #endregion

        public override void Initialize(int id)
        {
            StartReader(id);
            while (reader.Read())
            {
                this.ownerID = (int)reader["ownerID"];
                this.itemID = (int)reader["itemID"];
                this.storageID = (int)reader["storageID"];

                this.amount = (int)reader["amount"];
                this.profiency = (float)reader["profiency"];
                this.durability = (float)reader["durability"];
                this.isUnique = (bool)reader["isUnique"];

                this.prefixID = (int)reader["prefixID"];
                this.suffixID = (int)reader["suffixID"];
                this.retuningID = (int)reader["retuningID"];
                this.finalUpgradeType = (int)reader["finalUpgradeType"];
                this.finalUpgradeStep = (int)reader["finalUpgradeStep"];

                this.minDamage = (int)reader["minDamage"];
                this.maxDamage = (int)reader["maxDamage"];
                this.minWound = (int)reader["minWound"];
                this.maxWound = (int)reader["maxWound"];
                this.critical = (int)reader["critical"];
                this.stability = (int)reader["stability"];
                this.armorPierce = (int)reader["armorPierce"];
                this.protection = (int)reader["protection"];
                this.defence = (int)reader["defence"];
                this.magicDefence = (int)reader["magicDefence"];
                this.magicAffinity = (int)reader["magicAffinity"];
                this.chainCast = (int)reader["chainCast"];   
            }
            EndReader();
        }

        public override void Insert()
        {
            Database.Insert
           (
                this,
                ownerID,
                itemID,
                storageID,

                amount,
                profiency,
                durability,
                isUnique,

                prefixID,
                suffixID,
                retuningID,
                finalUpgradeType,
                finalUpgradeStep,

                minDamage,
                maxDamage,

                minWound,
                maxWound,

                critical,
                stability,
                armorPierce,

                protection,
                defence,
                magicDefence,

                magicAffinity,
                magicBalance,
                chainCast
           );
        }
        public override void Update(int packetID)
        {
            Database.Update
           (
                this,
                id,
                ownerID,
                itemID,
                storageID,

                amount,
                profiency,
                durability,
                isUnique,

                prefixID,
                suffixID,
                retuningID,
                finalUpgradeType,
                finalUpgradeStep,

                minDamage,
                maxDamage,

                minWound,
                maxWound,

                critical,
                stability,
                armorPierce,

                protection,
                defence,
                magicDefence,

                magicAffinity,
                magicBalance,
                chainCast
           );
        }
    }

    public class ItemDropEntry : DatabaseTable
    {
        [NOT_NULL] public int sceneID { get; set; }
        [NOT_NULL] public int itemEntryID { get; set; }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public override void Initialize(int id)
        {
            StartReader(id);
            while (reader.Read())
            {
                this.sceneID = (int)reader["sceneID"];
                this.itemEntryID = (int)reader["itemEntryID"];

                this.x = (float)reader["x"];
                this.y = (float)reader["y"];
                this.z = (float)reader["z"];
            }
            EndReader();
        }

        public override void Insert()
        {
            Database.Update(this, sceneID, itemEntryID, x, y, z);
        }
        public override void Update(int packetID)
        {
            Database.Update(this, id, sceneID, itemEntryID, x, y, z);
        }
    }

    public class SkillEntry : DatabaseTable
    {
        [NOT_NULL] public int ownerID { get; set; }
        [NOT_NULL] public int skillID { get; set; }

        public int skillRank { get; set; }
        public float exp { get; set; }

        public int skillTraining1 { get; set; }
        public int skillTraining2 { get; set; }
        public int skillTraining3 { get; set; }
        public int skillTraining4 { get; set; }
        public int skillTraining5 { get; set; }
        public int skillTraining6 { get; set; }
        public int skillTraining7 { get; set; }
        public int skillTraining8 { get; set; }
        public int skillTraining9 { get; set; }
        public int skillTraining10 { get; set; }

        public override void Initialize(int id)
        {
            StartReader(id);
            while (reader.Read())
            {
                this.ownerID = (int)reader["ownerID"];
                this.skillID = (int)reader["skillID"];

                this.skillRank = (int)reader["rank"];
                this.exp = (float)reader["exp"];

                this.skillTraining1 = (int)reader["skillTraining1"];
                this.skillTraining2 = (int)reader["skillTraining2"];
                this.skillTraining3 = (int)reader["skillTraining3"];
                this.skillTraining4 = (int)reader["skillTraining4"];
                this.skillTraining5 = (int)reader["skillTraining5"];
                this.skillTraining6 = (int)reader["skillTraining6"];
                this.skillTraining7 = (int)reader["skillTraining7"];
                this.skillTraining8 = (int)reader["skillTraining8"];
                this.skillTraining9 = (int)reader["skillTraining9"];
                this.skillTraining10 = (int)reader["skillTraining10"];
            }
            EndReader();
        }

        public override void Insert()
        {
            Database.Insert
            (
                this,
                ownerID,
                skillID,
                skillRank,
                exp,

                skillTraining1,
                skillTraining2,
                skillTraining3,
                skillTraining4,
                skillTraining5,
                skillTraining6,
                skillTraining7,
                skillTraining8,
                skillTraining9,
                skillTraining10
            );
        }

        public override void Update(int packetID)
        {
            Database.Update
            (
                this,
                id,
                ownerID,
                skillID,
                skillRank,
                exp,

                skillTraining1,
                skillTraining2,
                skillTraining3,
                skillTraining4,
                skillTraining5,
                skillTraining6,
                skillTraining7,
                skillTraining8,
                skillTraining9,
                skillTraining10
            );
        }
    }

    public class QuestEntry : DatabaseTable
    {
        [NOT_NULL] public int ownerID { get; set; }
        [NOT_NULL] public int questID { get; set; }
        public int taskAmount { get; set; }

        public override void Initialize(int id)
        {
            StartReader(id);
            while (reader.Read())
            {
                this.id = (int)reader["id"];
                this.ownerID = (int)reader["ownerID"];
                this.questID = (int)reader["questID"];
                this.taskAmount = (int)reader["taskAmount"];
            }
            EndReader();
        }

        public override void Insert()
        {
            throw new NotImplementedException();
        }

        public override void Update(int packetID)
        {
            throw new NotImplementedException();
        }
    }

    public class PetEntry : DatabaseTable
    {
        public int ownerID { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
        public int age { get; set; }

        public override void Initialize(int id)
        {
            StartReader(id);
            while (reader.Read())
            {
                this.ownerID = (int)reader["ownerID"];
                this.name = (string)reader["name"];
                this.level = (int)reader["level"];
                this.exp = (int)reader["exp"];
                this.age = (int)reader["age"];
                this.id = (int)reader["id"];
            }
            EndReader();
        }

        public override void Insert()
        {
            Database.Insert(this, ownerID, name, level, exp, age);
        }
        public override void Update(int packetID)
        {
            Database.Update(this, id, ownerID, name, level, exp, age);
        }
    }

    public class UmaData : DatabaseTable
    {
        public int ownerID { get; set; }
        public int bodyType { get; set; }
        public string pronouns { get; set; }

        public float height { get; set; }
        public float weight { get; set; }

        public override void Initialize(int id)
        {
            StartReader(id);
            while (reader.Read())
            {
                ownerID = (int)reader["ownerID"];
                height = (float)reader["height"];
                weight = (float)reader["weight"];
                bodyType = (int)reader["bodyType"];
                pronouns = (string)reader["pronouns"];
            }
            EndReader();
        }

        public override void Insert()
        {
            Database.Insert(this, ownerID, bodyType, pronouns, height, weight);
        }

        public override void Update(int packetID)
        {
            Database.Update(this, id, ownerID, bodyType, pronouns, height, weight);
        }
    }
}
