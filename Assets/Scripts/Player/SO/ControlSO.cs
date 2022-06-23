using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataControl", menuName = "ScriptableObjects/Data/ControlSO")]
public class ControlSO : ScriptableObject
{
    public float turnSpeed;
    public string verticalAxis;
    public string horizontalAxis;
}