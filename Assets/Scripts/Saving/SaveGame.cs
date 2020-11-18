using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Saving
{
    public class SaveGame : MonoBehaviour
    {
        public GameData game = new GameData(Scores.instance);
        public List<GameData> savedGames;
        private void Awake()
        {
            GameData.current = game;
        }
        private void Update()
        {
            savedGames = SaveLoad.savedGames;
        }
        private void OnGUI()
        {
            if (GUILayout.Button("Save"))
            {
                SaveLoad.Save(GameData.current);
                if (game.highScores.Length == 0)
                {
                    Debug.Log("no highscores");
                }
                for (int i = 0; i < game.highScores.Length; i++)
                {
                    Debug.Log(game.highScores[i]);
                }
            }
            if (GUILayout.Button("Load"))
            {
                SaveLoad.Load();
            }
            foreach (GameData _game in SaveLoad.savedGames)
            {

            }
        }
    }
}