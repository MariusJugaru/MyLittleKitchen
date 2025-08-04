using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [Header("Level config")]
    public List<ServeFoodScript.PlateRequirements> allPlates;
    public String currentLevel;

    public List<ServeFoodScript.PlateRequirements> nextLevel;
    public String nextLevelName;

    public void PlayLevel1()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("MainGame");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            GameObject servingStation = GameObject.Find("ServingStation");

            if (servingStation)
            {
                ServeFoodScript serveFood = servingStation.GetComponent<ServeFoodScript>();
                serveFood.allPlates = allPlates;
                serveFood.currentLevel = currentLevel;
                serveFood.nextLevel = nextLevel;
                serveFood.nextLevelName = nextLevelName;
                serveFood.isSet = true;
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
