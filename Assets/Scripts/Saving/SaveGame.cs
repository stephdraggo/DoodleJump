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
            game = new GameData(Scores.instance);
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