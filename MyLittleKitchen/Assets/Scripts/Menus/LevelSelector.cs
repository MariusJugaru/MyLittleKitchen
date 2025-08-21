using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [Header("Level config")]
    public int levelIdx = 0;

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
                serveFood.currentLevelIdx = levelIdx;
                serveFood.isSet = true;
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
