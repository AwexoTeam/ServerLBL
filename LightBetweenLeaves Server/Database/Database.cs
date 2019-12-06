﻿using DatabaseData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public static class Database
{
    public static MySqlConnection connection;
    private static string server;
    private static string database;
    private static string username;
    private static string password;

    public static Dictionary<Type, string> updateCMD;
    public static Dictionary<Type, string> insertIntoCMD;
    
    public static void Initialize()
    {
        updateCMD  = new Dictionary<Type, string>();
        insertIntoCMD  = new Dictionary<Type, string>();

        server = "remotemysql.com";
        username = "xwula2yOZ5";
        password = "Xh8SPcoG1K";
        database = "xwula2yOZ5";

        string conStr = "";
        conStr += "SERVER=" + server + ";";
        conStr += "DATABASE=" + database + ";";
        conStr += "UID=" + username + ";";
        conStr += "PASSWORD=" + password + ";";

        connection = new MySqlConnection(conStr);
        
        OpenConnection();

        var tables = typeof(DatabaseTable)
            .Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(DatabaseTable)) && !t.IsAbstract)
            .Select(t => (DatabaseTable)Activator.CreateInstance(t));
        
        foreach (var table in tables)
        {
            string cmdStr = table.GetCreateCmd();
            
            using (MySqlCommand cmd = new MySqlCommand(cmdStr, connection))
            {

                cmd.ExecuteNonQuery();
            }

            table.RegisterInsertCMD();
            table.RegisterUpdateCMD();
        }

    }

    private static bool OpenConnection()
    {
        try
        {
            connection.Open();
            Debug.LogWithTime(LogLevel.Verbose, "Database Initialized");
            return true;
        }
        catch (MySqlException e)
        {
            switch (e.Number)
            {
                case 0:
                    Debug.LogWithTime(LogLevel.Minimal, "Unable To initialize Database", true);
                    break;
                case 1045:
                    Debug.LogWithTime(LogLevel.Minimal, "Unable To initialize Database", true);
                    break;
                default:
                    break;
            }

            return false;
        }
    }

    private static bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException e)
        {
            Debug.LogWithTime(LogLevel.Minimal, e.Message, true);
            return false;
            throw;
        }
    }
    
    private static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class, IComparable<T>
    {
        List<T> objects = new List<T>();
        foreach (Type type in 
            Assembly.GetAssembly(typeof(T)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
        {
            objects.Add((T)Activator.CreateInstance(type, constructorArgs));
        }
        objects.Sort();
        return objects;
    }

    public static void Insert(DatabaseTable table, params object[] values)
    {
        if (insertIntoCMD.ContainsKey(table.GetType()))
        {
            string storedCmd = insertIntoCMD[table.GetType()];
            
            string cmdStr = string.Format(storedCmd, values);

            using (MySqlCommand cmd = new MySqlCommand(cmdStr))
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void Update(DatabaseTable table, params object[] values)
    {
        if (updateCMD.ContainsKey(table.GetType()))
        {
            string storedCmd = updateCMD[table.GetType()];
            string cmdStr = string.Format(storedCmd, values);
            
            using (MySqlCommand cmd = new MySqlCommand(cmdStr))
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
        }
        else { Debug.Log("Could not find update to"); }
    }
}
