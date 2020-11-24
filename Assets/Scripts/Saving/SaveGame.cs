using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Saving
{
    public class SaveGame : MonoBehaviour
    {
        public Scores scores;
        public GameData game;
        public List<GameData> savedGames;
        private void Awake()
        {
            scores = FindObjectOfType<Scores>();
            game = new GameData(scores);
            GameData.current = game;
        }
        private void Update()
        {
            savedGames = SaveLoad.savedGames;
        }
        public void SaveButton()
        {
            game = new GameData(scores); //get the current scores??
            SaveLoad.Save(GameData.current); //save the current game data
        }
        public void LoadButton()
        {
            SaveLoad.Load();

            for (int i = 0; i < game.highScores.Length; i++)
            {
                Debug.Log(game.highScores[i].score); //debug show me what scores are being loaded
            }
        }
        
        

        //private void OnGUI()
        //{
        //    if (GUILayout.Button("Save"))
        //    {
        //        SaveButton();
        //    }
        //    if (GUILayout.Button("Load"))
        //    {
        //        LoadButton();
        //    }
        //    foreach (GameData _game in SaveLoad.savedGames)
        //    {

        //    }
        //}
    }
}