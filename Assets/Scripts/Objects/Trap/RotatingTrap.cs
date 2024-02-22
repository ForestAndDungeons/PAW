using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTrap : MonoBehaviour
{
    [SerializeField] float _timeOffset;
    [SerializeField] float _rpm;
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, (Time.time - _timeOffset) * _rpm, 0);
    }
}
