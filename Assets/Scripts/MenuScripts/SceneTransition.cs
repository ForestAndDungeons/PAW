using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    string _sceneToLoad;
    int _scenesIndex;
    [SerializeField] float waitTime;
    [SerializeField] Animator transitionAnim;
    [SerializeField] Animator musicAnim;
    [SerializeField] List<string> _scenes;
    [SerializeField] List<string> _scenesLoaded;

    private void Start()
    {
        Time.timeScale = 1f;
        if(_scenes.Count > 0)
        {
            _scenesIndex = Random.Range(0, _scenes.Count);
            _sceneToLoad = _scenes[_scenesIndex];
            _scenesLoaded.Add(_scenes[_scenesIndex]);
        }
    }

    IEnumerator Transition(string sceneToLoad)
    {
        transitionAnim.SetTrigger("End");
        musicAnim.SetTrigger("fadeOut");
        _scenes.Remove(_sceneToLoad);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(_sceneToLoad);

    }

    public void ChangeScene()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        StartCoroutine(Transition(_sceneToLoad));
    }

    public void ReloadScene()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(_sceneToLoad);
    }

    /*public void LoadSceneFromSave(string sceneToLoad)
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        //sceneToLoad = PlayerPrefs.GetString(SaveData.CURRENT_LEVEL_KEY, sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }*/
    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}