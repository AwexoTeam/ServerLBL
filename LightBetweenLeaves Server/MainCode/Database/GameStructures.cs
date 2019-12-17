using DatabaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStructures
{
    public class MetricData : DatabaseTable
    {
        public int activatorID { get; set; }
        public string action { get; set; }
        public string value { get; set; }
        public string timeStamp { get; set; }
        
        public override void Initialize(int id)
        {
            StartReader(id);
            while (reader.Read())
            {
                activatorID = (int)reader["activatorID"];
                action = (string)reader["action"];
                value = (string)reader["value"];
                timeStamp = (string)reader["timeStamp"];
            }
            EndReader();
        }

        public override void Insert()
        {
            Database.Insert(this, activatorID, action, value, timeStamp);
        }
        public override void Update(int packetID)
        {
            Database.Update(this, id, activatorID, action, value, timeStamp);
        }
    }
}
