using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

//this is finished and does not need changing

namespace DoodleJump.Saving
{
    public static class SaveLoad
    {
        public static List<GameData> savedGames = new List<GameData>(); //static list of saved game states
        /// <summary>
        /// Save a GameData class to binary
        /// </summary>
        /// <param name="_game">the GameData being saved</param>
        public static void Save(GameData _game)
        {
            savedGames.Add(_game);
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.Create(Application.persistentDataPath + "/kittens.jpg"))
            {
                formatter.Serialize(file, SaveLoad.savedGames);
                file.Close();
            }
        }
        /// <summary>
        /// Loads all saved GameData classes from binary
        /// </summary>
        public static bool Load()
        {
            if (File.Exists(Application.persistentDataPath + "/kittens.jpg"))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream file = File.Open(Application.persistentDataPath + "/kittens.jpg", FileMode.Open))
                {
                    SaveLoad.savedGames = (List<GameData>)formatter.Deserialize(file);
                    file.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}