using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string levelToReload;

    [Header("Volume Settings")]
    [SerializeField] TMP_Text _volumeText;
    [SerializeField] Slider _volumeSlider;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void LoadLevelFromSave(string sceneToLoad)
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        //sceneToLoad = PlayerPrefs.GetString(SaveData.CURRENT_LEVEL_KEY, sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadLevel(string sceneToLoad)
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReloadScene()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(levelToReload);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        _volumeText.text = volume.ToString("0.0");
    }
}