using UnityEngine;

public class ControlsHintScript : MonoBehaviour
{
    public GameObject hintMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int controlsOn = PlayerPrefs.GetInt("ControlsMenu", 1);

        if (controlsOn == 0)
        {
            hintMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hintMenu.SetActive(!hintMenu.activeSelf);

            if (hintMenu.activeSelf)
                PlayerPrefs.SetInt("ControlsMenu", 1);
            else
                PlayerPrefs.SetInt("ControlsMenu", 0);
        }
            
    }
}
