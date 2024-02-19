using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }

    [Header("Components")]
    [SerializeField] Animator _musicAnim;
    public Animator musicAnim { get { return _musicAnim; } private set { } }

    [SerializeField] Animator _transitionAnim;
    public Animator transitionAnim { get { return _transitionAnim; } private set { } }

    [SerializeField] AudioSource _audioSource;
    public AudioSource audioSource { get { return _audioSource; } private set { } }

    [Header("Player")]
    public List<Player> _players;
    public bool isSinglePlayer;
    public GameObject _player1Prefab;
    public GameObject _player2Prefab;

    [Header("SceneTransition")]
    SceneTransition _sceneTransition;
    public SceneTransition sceneTransition { get { return _sceneTransition; } private set { } }

    [SerializeField] SceneTransitionOS _sceneTransitionOS;
    public SceneTransitionOS sceneTransitionOS { get { return _sceneTransitionOS; } private set { } }

    [SerializeField] List<string> _scenes;
    public List<string> scenes {get { return _scenes; } private set { } }

    [SerializeField] List<string> _scenesLoaded;
    public List<string> scenesLoaded { get { return _scenesLoaded; } private set { } }

    string _sceneToLoad;
    public string sceneToLoad { get { return _sceneToLoad; } private set { } }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        _sceneTransition = GetComponentInChildren<SceneTransition>();
        //_sceneTransition = new SceneTransition();

        RestartList();
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        
    }

    public void RestartList()
    {
        _scenes.Clear();
        _scenesLoaded.Clear();

        if(_scenes.Count <= 0)
        {
            foreach (var scene in sceneTransitionOS._allLevels)
                _scenes.Add(scene);
        }
    }

    public void ChooseRandomScene()
    {
        if (_scenes.Count > 0)
        {
            var i = Random.Range(0, _scenes.Count);
            _sceneToLoad = _scenes[i];
            _scenesLoaded.Add(_scenes[i]);
            _scenes.Remove(_scenes[i]);
        }
        else
            _sceneToLoad = "Victory Screen";
    }

    public void ChangeMusic(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.volume = 0f;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
