using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] float waitTime;

    [SerializeField] SceneTransitionOS _sceneTransitionOS;
    [SerializeField] List<string> _scenes;
    [SerializeField] List<string> _scenesLoaded;
    string _sceneToLoad;
    int _scenesIndex;

    [SerializeField] string _mainMenu;
    [SerializeField] string _victoryScreen;

    [SerializeField] Animator transitionAnim;
    [SerializeField] Animator musicAnim;

    AudioSource _audioSource;
    [SerializeField] AudioClip _mainMenuMusic;
    [SerializeField] AudioClip _levelMusic;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        RestartList();
    }

    public void Start()
    {
        Time.timeScale = 1f;

        if (_scenes.Count > 0)
        {
            UpdateList();
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeScene()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        StartCoroutine(Transition(_sceneToLoad));
    }

    public void UpdateList()
    {
        if(_scenes.Count > 0)
        { 
            _scenesIndex = Random.Range(0, _scenes.Count);
            _sceneToLoad = _scenes[_scenesIndex];
            _scenesLoaded.Add(_scenes[_scenesIndex]);
            _scenes.Remove(_scenes[_scenesIndex]); 
        }
        else
            SceneManager.LoadScene(_victoryScreen);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(_mainMenu);
        _scenesLoaded.Clear();
        RestartList();

        _audioSource.Stop();
        _audioSource.volume = 0f;
        _audioSource.clip = _mainMenuMusic;
        _audioSource.Play();
        Destroy(this.gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartList()
    {
        foreach (var scene in _sceneTransitionOS._allLevels)
            _scenes.Add(scene);
    }

    public void OnTriggerEnter(Collider other)
    {
        UpdateList();
        ChangeScene();
    }

    IEnumerator Transition(string sceneToLoad)
    {
        transitionAnim.SetTrigger("End");
        musicAnim.SetTrigger("fadeOut");
        _scenes.Remove(_sceneToLoad);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(_sceneToLoad);
        _audioSource.clip = _levelMusic;
        _audioSource.Play();
        _audioSource.volume = 0f;
    }
}