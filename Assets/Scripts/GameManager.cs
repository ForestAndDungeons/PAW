using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    public List<Player> _players;
    bool isPaused;
    [SerializeField] GameObject _pauseMenu;

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
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        
    }

    public void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            _pauseMenu.SetActive(false);
            isPaused = false;
        }
        else if (!isPaused)
        {
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
            isPaused = true;
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
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
