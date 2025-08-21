using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private List<ServeFoodScript.PlateRequirements> allPlates;
    private int currentLevelIdx;

    void Start()
    {
        GameObject servingStation = GameObject.Find("ServingStation");

        if (servingStation)
        {
            ServeFoodScript serveFood = servingStation.GetComponent<ServeFoodScript>();

            StartCoroutine(WaitForSet(serveFood));
        }

    }

    IEnumerator WaitForSet(ServeFoodScript serveFood)
    {
        yield return new WaitUntil(() => serveFood.isSet);

        currentLevelIdx = serveFood.currentLevelIdx;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
    }

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(currentSceneName);

        ItemHandler.isEquipment = false;
        ItemHandler.isItem = false;

        Resume();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            GameObject servingStation = GameObject.Find("ServingStation");

            if (servingStation)
            {
                ServeFoodScript serveFood = servingStation.GetComponent<ServeFoodScript>();
                serveFood.currentLevelIdx = currentLevelIdx;
                serveFood.isSet = true;
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void NextLevel()
    {
        GameObject servingStation = GameObject.Find("ServingStation");

        if (servingStation)
        {
            currentLevelIdx++;

            Restart();
        }
    }
}
