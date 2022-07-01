using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    SceneTransition _sceneManager;

    public void Awake()
    {
        _sceneManager = FindObjectOfType<SceneTransition>();
    }

    public void OnTriggerEnter(Collider other)
    {
        _sceneManager.UpdateList();
        _sceneManager.ChangeScene();
    }
}
