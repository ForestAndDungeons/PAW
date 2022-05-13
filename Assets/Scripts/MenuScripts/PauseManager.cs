using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Settings")]
    bool _isPaused;
    [SerializeField] GameObject _pauseMenuUI;
    [SerializeField] KeyCode _pauseKey;
    [SerializeField] GameObject[] _players;

    void Awake()
    {
        _isPaused = false;
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
        }
        else
        {
            Time.timeScale = 1f;
            _pauseMenuUI.SetActive(false);
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