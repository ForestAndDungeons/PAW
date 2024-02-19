using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    SceneTransition _sceneManager;
    [SerializeField] string _victoryScreen;

    public void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.ChooseRandomScene();
        GameManager.Instance.sceneTransition.Victory(_victoryScreen);
        //_sceneManager.ChangeScene();
    }
}
