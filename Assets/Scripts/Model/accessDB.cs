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
        internal static PlayerCharacter accessCharacterDatabase(string theClass)
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