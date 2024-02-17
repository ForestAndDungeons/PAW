using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Classes")]
    SceneTransition _sceneTransition;

    public SceneTransition sceneTransition { get { return _sceneTransition; } private set { } }

    public List<Player> _players;
    public bool isSinglePlayer;
    public GameObject _player1Prefab;
    public GameObject _player2Prefab;

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
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
