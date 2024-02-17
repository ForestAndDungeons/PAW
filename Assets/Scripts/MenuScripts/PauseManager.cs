using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Settings")]
    bool _isPaused;
    AudioSource _audioSource;
    SceneTransition _sceneTransition;
    [SerializeField] AudioClip[] _audioClip;
    [SerializeField] GameObject _pauseMenuUI;
    [SerializeField] KeyCode _pauseKey;
    [SerializeField] GameObject[] _players;
    [SerializeField] GameObject _bossLifeBar;

    [SerializeField] GameObject _resumeButton;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _defeatScreen;

    void Awake()
    {
        _isPaused = false;
        _audioSource = GetComponent<AudioSource>();
        //_sceneTransition = FindObjectOfType<SceneTransition>();
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
        _pauseKey = KeyCode.KeypadDivide;

        Time.timeScale = 0f;
        _pauseMenuUI.SetActive(true);
        _resumeButton.SetActive(false);
        _pausePanel.SetActive(false);
        _defeatScreen.SetActive(true);
        _bossLifeBar.SetActive(false);

    }

    public void SceneTransitionGoToMenu()
    {
        GameManager.Instance.sceneTransition.GoToMenu();
        //_sceneTransition.GoToMenu();
    }
}