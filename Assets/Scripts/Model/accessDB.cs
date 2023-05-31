using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace DefaultNamespace
{

    /// <summary>
    /// This class will be a representation of a stat change or active effect on an Actor.
    /// </summary>
    public class accessDB : MonoBehaviour
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

        internal static int BuffDatabaseDuration(string theClass)
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
                        int duration = dataReader.GetInt32(2);
                        dbConnection.Close();
                        return duration;
                    }
                }
                throw new System.Exception("Duration for buff not found.");
            }
        }

        internal static double BuffDatabasePercentage(string theClass)
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
                        double percentage = dataReader.GetDouble(5);
                        dbConnection.Close();
                        return percentage;
                    }
                }
                throw new System.Exception("Percentage for buff not found.");
            }
        }

        internal static string BuffDatabaseStatModified(string theClass)
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
                        string statModified = dataReader.GetString(3);
                        dbConnection.Close();
                        return statModified;
                    }
                }
                throw new System.Exception("Stat for buff not found.");
            }
        }

        internal static int BuffDatabaseManaCost(string theClass)
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
                        int manaCost = dataReader.GetInt32(4);
                        dbConnection.Close();
                        return manaCost;
                    }
                }
                throw new System.Exception("Stat for buff not found.");
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