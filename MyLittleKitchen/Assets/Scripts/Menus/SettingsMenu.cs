using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;

    public TMPro.TMP_Dropdown resolutionDropdown;
    public Slider slider;
    public GameObject on;
    public GameObject off;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIdx = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIdx = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        int resolution = PlayerPrefs.GetInt("ResolutionPref", currentResIdx);
        resolutionDropdown.value = resolution;
        SetResolution(resolution);
        resolutionDropdown.RefreshShownValue();

        float volume = PlayerPrefs.GetFloat("VolumePref", 0);
        SetVolume(volume);
        slider.value = volume;

        int fullscreen = PlayerPrefs.GetInt("FullscreenPref", 1);
        if (fullscreen == 0)
        {
            on.SetActive(true);
            off.SetActive(false);
            SetFullscreen(false);
        }

    }

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("VolumePref", volume);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            PlayerPrefs.SetInt("FullscreenPref", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullscreenPref", 0);
        }
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIdx)
    {
        Resolution res = resolutions[resolutionIdx];
        PlayerPrefs.SetInt("ResolutionPref", resolutionIdx);
        PlayerPrefs.Save();

        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
