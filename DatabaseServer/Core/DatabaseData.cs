﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseData
{
    public class IGNORE : Attribute { }
    public class NOT_NULL : Attribute { }
    public class PRIMARY_KEY : Attribute { }
    public class AUTO_INCREMENT : Attribute { }
    public class DONT_UPDATE : Attribute { }

    public abstract class DatabaseTable
    {
        [PRIMARY_KEY]
        [AUTO_INCREMENT]
        [NOT_NULL]
        public int id { get; set; }

        public virtual string GetCreateCmd()
        {
            string cmd = "";
            cmd = "CREATE TABLE IF NOT EXISTS " + GetType().Name + "(\n";

            PropertyInfo[] properties = GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                string line = "";

                string name = property.Name;
                string typeName = GetTypeString(property);

                line += name + " " + typeName + " ";

                foreach (Attribute attr in Attribute.GetCustomAttributes(property))
                {
                    if (!property.IsDefined(typeof(IGNORE)))
                    {
                        if (attr.GetType() == typeof(AUTO_INCREMENT))
                        {
                            line += attr.GetType().Name + " ";
                        }
                        else
                        {
                            line += attr.GetType().Name.Replace('_', ' ') + " ";
                        }
                    }
                }

                if (i != 0)
                    cmd += ",";

                cmd += line + "\n";
            }

            cmd += ");";
            return cmd;
        }

        public virtual void RegisterInsertCMD()
        {
            string cmd = "";
            cmd = "INSERT INTO " + GetType().Name + "(";

            PropertyInfo[] properties = GetType().GetProperties();
            List<PropertyInfo> tempList = new List<PropertyInfo>();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo p = properties[i];
                if (p.IsDefined(typeof(IGNORE))) { }
                else if (p.IsDefined(typeof(PRIMARY_KEY))) { }
                else { tempList.Add(p); }
            }
            properties = tempList.ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo p = properties[i];
                cmd += p.Name;
                if (i < properties.Length - 1) { cmd += ","; }
            }
            cmd += ")\n";
            cmd += "VALUES(";

            for (int j = 0; j < properties.Length; j++)
            {
                if (properties[j].PropertyType == typeof(string))
                {
                    cmd += "'{" + j + "}'";
                }
                else
                {
                    cmd += "{" + j + "}";
                }

                if (j < properties.Length - 1) { cmd += ","; }
            }
            cmd += ")\n";

            Database.insertIntoCMD.Add(GetType(), cmd);
        }
        public virtual void RegisterUpdateCMD()
        {
            string cmd = "";
            string primaryKey = "";

            cmd = "UPDATE " + GetType().Name + "\n";
            cmd += " SET \n";

            int cnt = 0;

            List<PropertyInfo> properties = new List<PropertyInfo>();
            properties.AddRange(GetType().GetProperties());

            primaryKey = properties.Find(p => p.IsDefined(typeof(PRIMARY_KEY))).Name;

            properties.RemoveAll(p => p.IsDefined(typeof(PRIMARY_KEY)));
            properties.RemoveAll(p => p.IsDefined(typeof(AUTO_INCREMENT)));
            properties.RemoveAll(p => p.IsDefined(typeof(IGNORE)));
            properties.RemoveAll(p => p.IsDefined(typeof(DONT_UPDATE)));

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyInfo p = properties[i];

                cnt++;
                cmd += p.Name + " = ";

                if (p.PropertyType == typeof(string))
                {
                    cmd += "'{" + cnt + "}'";
                }
                else
                {
                    cmd += "{" + cnt + "}";
                }
                if (i < properties.Count - 1) { cmd += ","; }
            }

            cmd += " WHERE\n ";
            cmd += primaryKey + " = {0};";

            Database.updateCMD.Add(GetType(), cmd);
        }

        public abstract void Initialize(int id);
        public abstract void Update(int id);
        public abstract void Insert();
        
        public MySqlDataReader reader;

        public void StartReader(int _id, string valName = "id")
        {
            id = _id;
            StartReader("SELECT * FROM `" + GetType().Name + "` WHERE "+valName+" = " + id);
        }
        public void StartReader(string cmdStr)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Database.connection;
                cmd.CommandText = cmdStr;
                
                reader = cmd.ExecuteReader();
            }
        }
        public void EndReader()
        {
            reader.Close();
        }

        private string GetTypeString(PropertyInfo property)
        {
            if (property.PropertyType == typeof(int)) { return "INT"; }
            if (property.PropertyType == typeof(float)) { return "FLOAT"; }
            if (property.PropertyType == typeof(string)) { return "TEXT"; }
            if (property.PropertyType == typeof(bool)) { return "BOOLEAN"; }

            else { return ""; }
        }
    }
}
