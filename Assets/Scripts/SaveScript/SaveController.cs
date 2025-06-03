using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
    {
        public string saveLocation;

        void Start()
        {
            saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (GameState.isLoadingFromSave)
        {
            LoadGame(applySavedPosition: false); // Don't override spawn
            GameState.isLoadingFromSave = false;
        }
        
        }

        public void SaveGame()
        {
            SaveData saveData = new SaveData
            {
                playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position
            };

            File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        }

    /*public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;

        }
        else
        {
            SaveGame();
        }
    }*/
    public void LoadGame(bool applySavedPosition = true)
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            if (applySavedPosition)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            }
        }
        else
        {
            SaveGame(); // Create a new save if none exists
        }
    }

}