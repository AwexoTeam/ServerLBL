using CharacterStructures;
using GameDefinations;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum LoginErrorCode
{
    Successful,
    WrongPassword,
    UsernameTaken,
    InvalidName,
    Unknown,
}

public  class DatabaseHandler : Initializable
{
    public static MySqlConnection connection => Database.connection;

    public static string username = "";
    public static string password = "";
    public static string server = "";
    public static string database;

    public int priority => 1;

    public void Initialize()
    {
        Database.Initialize(server, database, username, password);
    }

    public static LoginAnswer DoLoginCheck(int connectionId, string _username, string _password)
    {
        bool hasCharacter = false;
        bool canLogin = false;
        LoginErrorCode errorCode = LoginErrorCode.Unknown;
        int characterID = -1;

        string cmdStr = "SELECT * FROM `Account` WHERE username = '" + _username + "'";
        using (MySqlCommand cmd = new MySqlCommand(cmdStr, connection))
        {
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        canLogin = (string)reader["password"] == _password;
                        if (canLogin)
                        {
                            characterID = (int)reader["id"];
                            errorCode = LoginErrorCode.Successful;
                            string name = (string)reader["name"];
                            hasCharacter = name != string.Empty;

                            EventArgs args = new EventArgs(GameEventType.OnPlayerLogin, hasCharacter);
                            EventHandler.InvokeEvent(args);
                        }
                        else
                        {
                            errorCode = LoginErrorCode.WrongPassword;
                        }
                    }
                }
                else
                {
                    errorCode = LoginErrorCode.InvalidName;
                }
            }
        }

        if (!canLogin && errorCode == LoginErrorCode.InvalidName)
        {
            //TODO: base account stuff 
            Account account = new Account();
            account.username = _username;
            account.password = _password;

            account.Insert();
        }

        LoginAnswer answer = new LoginAnswer()
        {
            id = connectionId,
            canLogin = canLogin,
            hasCharacter = hasCharacter,
            errorCode = (int)errorCode,
            characterID = characterID
        };

        return answer;
    }

    public static bool DoesCharacterNameExist(string name)
    {
        string cmdStr = "SELECT * FROM `Account` WHERE name = '" + name+ "'";
        using(MySqlCommand cmd = new MySqlCommand(cmdStr, connection))
        {
            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                return reader.HasRows;
            }
        }
    }
}
