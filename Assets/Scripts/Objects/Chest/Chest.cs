using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] List<GameObject> _listObjects;
    [SerializeField] float _randomFloat;
    [SerializeField] Transform _spawnObject;

    private void Awake()
    {
        _randomFloat = Random.Range(0, _listObjects.Count);
    }
    public void OpenChest(Transform transform)
    {
        if (_listObjects.Count > 0)
        {
            Debug.Log("open chest, el transform es " + transform);
            for (int i = 0; i < _listObjects.Count; i++)
            {
                int index = _listObjects.IndexOf(_listObjects[i]);
                if (_randomFloat == index)
                {
                    
                    Debug.Log("El objeto a instanciar es : " + _listObjects[i].name);
                    Instantiate(_listObjects[i], _spawnObject.position , Quaternion.identity);

                }
            }
        }
        
    }
}
