using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace DungeonAdventure
{

    /// <summary>
    /// This class handles SQLite Database access and logic.
    /// </summary>
    public class AccessDB : MonoBehaviour
    {

        /// <summary>
        /// Constructs a PlayerCharacter by accessing the SQLite database with a string
        /// that represents the CharacterClass to be created.
        /// </summary>
        /// <param name="theClass">The CharacterClass to attempt to create a PlayerCharacter of.</param>
        /// <returns>A new PlayerCharacter object with values from the SQLite database.</returns>
        internal static PlayerCharacter PlayerDatabaseConstructor(in string theClass)
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

        /// <summary>
        /// Constructs an EnemyCharacter by accessing the SQLite database with a string
        /// that represents the CharacterClass to be created.
        /// </summary>
        /// <param name="theClass">The CharacterClass to attempt to create an EnemyCharacter of.</param>
        /// <returns>A new EnemyCharacter object with values from the SQLite database.</returns>
        internal static EnemyCharacter EnemyDatabaseConstructor(in string theClass)
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

        /// <summary>
        /// Constructs a Buff object by accessing the SQLite database with a string
        /// that represents the CharacterClass to be based on.
        /// </summary>
        /// <param name="theClass">The CharacterClass to attempt to create an Buff from.</param>
        /// <returns>A new Buff object with values from the SQLite database.</returns>
        internal static Buff BuffDatabaseConstructor(in string theClass)
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


        /// <summary>
        /// Constructs a SpecialAttack object by accessing the SQLite database with a string
        /// that represents the CharacterClass to be based on.
        /// </summary>
        /// <param name="theClass">The CharacterClass to attempt to create a Special Attack from.</param>
        /// <returns>A new Special Attack object with values from the SQLite database.</returns>
        internal static SpecialAttack SpecialAttackDatabaseConstructor(in string theClass)
        {
            IDbConnection dbConnection = OpenDatabase();
            IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
            dbCommandReadValues.CommandText = "SELECT * FROM specialAttackTemplates;";
            IDataReader dataReader = dbCommandReadValues.ExecuteReader();
            {

                while (dataReader.Read())
                {

                    if (dataReader.GetString(0) == theClass.ToLower())
                    {
                        SpecialAttack special = new SpecialAttack(dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetDouble(5),
                        dataReader.GetString(3), dataReader.GetInt32(4), dataReader.GetDouble(6));
                        dbConnection.Close();
                        return special;
                    }
                }
                Debug.Log(theClass);
                throw new System.Exception("Class provided for Special Attack not found.");
            }
        }


        /// <summary>
        /// A static method for connecting to the SQLite database.
        /// </summary>
        /// <returns>A connection to the SQLite database.</returns>
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