using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    string _sceneToLoad;
    [SerializeField] Animator transitionAnim;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    IEnumerator Transition(string sceneToLoad)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ChangeScene(string sceneToLoad)
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        StartCoroutine(Transition(sceneToLoad));
    }

    public void ReloadScene()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(_sceneToLoad);
    }

    public void LoadSceneFromSave(string sceneToLoad)
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        //sceneToLoad = PlayerPrefs.GetString(SaveData.CURRENT_LEVEL_KEY, sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}