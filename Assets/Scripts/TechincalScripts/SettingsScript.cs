﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public static SettingsScript instance;

    [Header("Resolution")]
    Resolution[] resolutions;
    [SerializeField] private Dropdown resolutionDropdown;
    private int currentResolutionIndex = 0;

    [Header("Screen Modes")]
    [SerializeField] private Dropdown screenModesDropdown;

    [Header("Cheats")]
    [SerializeField] private Toggle godMode;

    private void Awake()
    {
        instance = this;
        if (AudioManager.instance != null)
        {
            SetMusicLevel(AudioManager.instance.musicLevel);
            SetSoundLevel(AudioManager.instance.soundLevel);
        }
        SetFullscreen(true);

        if (Game.instance != null)
        {
            SetGodMode(Game.instance.GodModeActivated);
        }
    }

    private void Start()
    {
        ResolutionSetup();
        ScreenModeSetup();
    }

    public void SetMusicLevel(float sliderValue)
    {
        AudioManager.instance.musicLevel = sliderValue;
        AudioManager.instance.UpdateMixer();
    }

    public void SetSoundLevel(float sliderValue)
    {
        AudioManager.instance.soundLevel = sliderValue;
        AudioManager.instance.UpdateMixer();
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (!isFullScreen)
        {
            Resolution resolution = Screen.currentResolution;
            Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
            screenModesDropdown.interactable = true;
        }
        else
        {
            screenModesDropdown.interactable = false;
        }
    }

    public void SetGodMode(bool godModeValue)
    {
        // Change UI if players exits from game, but cheats were activated
        godMode.isOn = godModeValue;
        Game.instance.GodModeActivated = godModeValue;
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void SetScreenMode(int screenMode)
    {
        Screen.fullScreenMode = (FullScreenMode) screenMode;
    }

    public void SetVSync(int vSyncValue)
    {
        QualitySettings.vSyncCount = vSyncValue;
    }

    public void ResolutionSetup()
    {
        // Get all possible resolutions and set current as default
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add(resolutions[i].width + "x" + resolutions[i].height + " @ " + resolutions[i].refreshRate + "Hz");

            if (resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ScreenModeSetup()
    {
        screenModesDropdown.ClearOptions();
        List<string> fullScreenOptions = new List<string>();
        foreach (FullScreenMode screenMode in FullScreenMode.GetValues(typeof(FullScreenMode)))
        {
            fullScreenOptions.Add(screenMode.ToString());
        }
        screenModesDropdown.AddOptions(fullScreenOptions);
        screenModesDropdown.value = 0;
        resolutionDropdown.RefreshShownValue();
    }
}
