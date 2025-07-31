using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public List<ServeFoodScript.PlateRequirements> allPlates;
    public String currentLevel;

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
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
