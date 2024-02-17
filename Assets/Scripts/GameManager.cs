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

    /*IEnumerator LoadSceneTimer()
    {
        Time.timeScale = 1f;

        yield return new WaitForSeconds(1f);
        //_myAnimator.SetTrigger("Fade");

        yield return new WaitForSeconds(1f);

        //_player.playerBase.SetCurrentHealth(_player.playerBase.maxHealth);
        //_player.playerBase.SetIsDead(false);

        SceneManager.LoadScene(($"Level_1 {Random.Range(1, 4)}"));
    }*/

    public void ExitGame()
    {
        Application.Quit();
    }
}
