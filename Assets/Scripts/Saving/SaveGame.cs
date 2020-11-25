﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Saving
{
    public class SaveGame : MonoBehaviour
    {
        public ScoresNew scores;
        public GameData game;
        public List<GameData> savedGames;
        private void Awake()
        {
            scores = FindObjectOfType<ScoresNew>();
            game = new GameData(scores);
            GameData.current = game;
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
        public void LoadButton()
        {
            SaveLoad.Load();

            if (game.highScores == null)
                return;

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