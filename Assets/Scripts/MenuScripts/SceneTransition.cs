using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] float waitTime;

    [SerializeField] string _mainMenu;
    [SerializeField] string _victoryScreen;

    [SerializeField] AudioClip _mainMenuMusic;
    [SerializeField] AudioClip _levelMusic;
    [SerializeField] AudioClip _victoryMusic;

    public void ChangeScene()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        GameManager.Instance.ChooseRandomScene();
        StartCoroutine(Transition(GameManager.Instance.sceneToLoad));
    }

    public void Victory(string victoryScreen)
    {
        SceneManager.LoadScene(_victoryScreen);
    }

    IEnumerator Transition(string sceneToLoad)
    {
        GameManager.Instance.transitionAnim.SetTrigger("End");
        GameManager.Instance.musicAnim.SetTrigger("fadeOut");
        GameManager.Instance.scenes.Remove(GameManager.Instance.sceneToLoad);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(GameManager.Instance.sceneToLoad);
        
        GameManager.Instance.ChangeMusic(_levelMusic);
    }

    public void IsSinglePlayer(bool value)
    {
        GameManager.Instance.isSinglePlayer = value;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(_mainMenu);
        GameManager.Instance.scenesLoaded.Clear();
        GameManager.Instance.RestartList();

        GameManager.Instance.ChangeMusic(_mainMenuMusic);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}