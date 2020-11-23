using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Saving
{
    public class SaveGame : MonoBehaviour
    {
        public GameData game;
        public List<GameData> savedGames;
        private void Awake()
        {
            game = new GameData(FindObjectOfType<Scores>());
            GameData.current = game;
        }
        private void Update()
        {
            savedGames = SaveLoad.savedGames;
        }
        public void SaveButton()
        {
            SaveLoad.Save(GameData.current);
        }
        public void LoadButton()
        {
            SaveLoad.Load();
            for (int i = 0; i < game.highScores.Length; i++)
            {
                Debug.Log(game.highScores[i].score);
            }
        }
        private void OnGUI()
        {
            if (GUILayout.Button("Save"))
            {
                SaveButton();
            }
            if (GUILayout.Button("Load"))
            {
                LoadButton();
            }
            foreach (GameData _game in SaveLoad.savedGames)
            {

            }
        }
    }
}