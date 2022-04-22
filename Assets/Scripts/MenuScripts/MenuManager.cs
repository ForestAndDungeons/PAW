using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] TMP_Text _volumeText = null;
    [SerializeField] Slider _volumeSlider = null;

    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void ExitButton()
    {
        Debug.Log("Sali del Juego");
        Application.Quit(); 
    }

    public void SetVolume(float volume)
    {
        _volumeText.text = volume.ToString("0.0");
    }

}
