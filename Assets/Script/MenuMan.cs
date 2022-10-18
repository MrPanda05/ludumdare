using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuMan : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] List<GameObject> UI;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            Load();
        }
        else
        {
            Load();
        }
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            UI[2].SetActive(false);
        }
    }

    public void KillGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToSettings()
    {
        UI[0].SetActive(false);
        UI[1].SetActive(true);
    }

    public void GoToMenu()
    {
        UI[0].SetActive(true);
        UI[1].SetActive(false);
    }
    public void ChangeValue()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
    }
}

