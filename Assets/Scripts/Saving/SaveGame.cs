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
        }
        private void Start()
        {
            GameData.current = scores.game;
        }
        private void Update()
        {
            savedGames = SaveLoad.savedGames;
        }
        public void SaveButton(GameData _game)
        {
            //game = new GameData(scores); //get the current scores??
            SaveLoad.Save(_game); //save the passed game data
        }
        public bool LoadButton()
        {
           bool isLoaded= SaveLoad.Load();
            if (!isLoaded)
            {
                return isLoaded;
            }
            
            if (game.highScores == null)
                return false;

            for (int i = 0; i < game.highScores.Length; i++)
            {
                Debug.Log(game.highScores[i].score); //debug show me what scores are being loaded
            }
            return isLoaded;
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