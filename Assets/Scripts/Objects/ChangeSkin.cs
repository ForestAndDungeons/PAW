using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ChangeSkin : MonoBehaviour
{
    [SerializeField] TileSkin _object;
    [SerializeField] [Range(1, 10)] int _skin = 1;

    void OnValidate()
    {
        if (_skin > _object.mesh.Length)
            _skin = _object.mesh.Length;
        _object.ChangeSkin(_skin);
    }
}