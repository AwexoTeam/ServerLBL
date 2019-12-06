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
        [PRIMARY_KEY]
        [AUTO_INCREMENT]
        [NOT_NULL]
        public int id { get; set; }

        [NOT_NULL] public string name { get; set; }
        [NOT_NULL] public string password { get; set; }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public override void Initialize(int id)
        {
            MySqlDataReader reader;
            reader = GetReader("SELECT * FROM `" + GetType().Name + "` WHERE id = " + id);

            while (reader.Read())
            {
                this.id = (int)reader["id"];

                this.name = (string)reader["name"];
                this.x = (float)reader["x"];
                this.y = (float)reader["y"];
                this.z = (float)reader["z"];
            }

            reader.Close();
        }
        public override void Insert()
        {
            Database.Insert(this, name, x, y, z);
        }
        public override void Update()
        {
            Database.Update(this, id, name, x, y, z);
        }
    }
}
