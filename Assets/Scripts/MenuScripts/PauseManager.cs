using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Settings")]
    bool _isPaused;
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;
    [SerializeField] GameObject _pauseMenuUI;
    [SerializeField] KeyCode _pauseKey;
    [SerializeField] GameObject[] _players;

    void Awake()
    {
        _isPaused = false;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(_pauseKey) && _players != null)
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused == true)
        {
            Time.timeScale = 0f;
            _pauseMenuUI.SetActive(true);
            _audioSource.PlayOneShot(_audioClip[0]);
        }
        else
        {
            Time.timeScale = 1f;
            _pauseMenuUI.SetActive(false);
            _audioSource.PlayOneShot(_audioClip[1]);
        }
    }

    public void TogglePermanentPause()
    {
        _isPaused = true;
        //TEMPORAL
        _pauseKey = KeyCode.L;

        Time.timeScale = 0f;
        _pauseMenuUI.SetActive(true);
    }
}