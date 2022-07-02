using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    SceneTransition _sceneManager;
    [SerializeField] string _victoryScreen;

    public void Awake()
    {
        _sceneManager = FindObjectOfType<SceneTransition>();
    }

    public void OnTriggerEnter(Collider other)
    {
        _sceneManager.UpdateList();
        _sceneManager.Victory(_victoryScreen);
        //_sceneManager.ChangeScene();
    }
}
