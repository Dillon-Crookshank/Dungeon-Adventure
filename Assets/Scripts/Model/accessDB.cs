using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace DungeonAdventure
{

    /// <summary>
    /// This class will be a representation of a stat change or active effect on an Actor.
    /// </summary>
    public class AccessDB : MonoBehaviour
    {
        internal static PlayerCharacter PlayerDatabaseConstructor(string theClass)
        {
            IDbConnection dbConnection = OpenDatabase();
            IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
            dbCommandReadValues.CommandText = "SELECT * FROM characterTemplates;";
            IDataReader dataReader = dbCommandReadValues.ExecuteReader();
            // specify which entry to grab
            {

                while (dataReader.Read())
                {

                    if (dataReader.GetString(0) == theClass.ToLower())
                    {
                        PlayerCharacter character = new PlayerCharacter(dataReader.GetString(0), dataReader.GetFloat(1),
                        dataReader.GetFloat(2), dataReader.GetFloat(3), dataReader.GetFloat(4), dataReader.GetInt32(5));
                        dbConnection.Close();
                        return character;
                    }
                }
                throw new System.Exception("PlayerCharacter " + theClass + " not found.");
            }

        }

        internal static EnemyCharacter EnemyDatabaseConstructor(string theClass)
        {
            IDbConnection dbConnection = OpenDatabase();
            IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
            dbCommandReadValues.CommandText = "SELECT * FROM enemyTemplates;";
            IDataReader dataReader = dbCommandReadValues.ExecuteReader();
            // specify which entry to grab
            {

                while (dataReader.Read())
                {

                    if (dataReader.GetString(0) == theClass.ToLower())
                    {
                        EnemyCharacter enemy = new EnemyCharacter(dataReader.GetString(0), dataReader.GetFloat(1),
                        dataReader.GetFloat(2), dataReader.GetFloat(3), dataReader.GetFloat(4), dataReader.GetInt32(5));
                        dbConnection.Close();
                        return enemy;
                    }
                }
                throw new System.Exception("EnemyCharacter " + theClass + " not found.");
            }

        }

        internal static Buff BuffDatabaseConstructor(string theClass)
        {
            IDbConnection dbConnection = OpenDatabase();
            IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
            dbCommandReadValues.CommandText = "SELECT * FROM buffTemplates;";
            IDataReader dataReader = dbCommandReadValues.ExecuteReader();
            {

                while (dataReader.Read())
                {

                    if (dataReader.GetString(0) == theClass.ToLower())
                    {
                        Buff buff = new Buff(dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetDouble(5),
                        dataReader.GetString(3), dataReader.GetInt32(4));
                        dbConnection.Close();
                        return buff;
                    }
                }
                Debug.Log(theClass);
                throw new System.Exception("Class provided for Buff not found.");
            }
        }


        private static IDbConnection OpenDatabase()
        {
            // Open a connection to the database.
            string dbUri = "URI=file:" + Application.dataPath + "/CharacterDatabase.sqlite";
            IDbConnection dbConnection = new SqliteConnection(dbUri);
            dbConnection.Open();

            return dbConnection;
        }

    }

}