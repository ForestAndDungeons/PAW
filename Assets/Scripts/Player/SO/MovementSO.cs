using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataMovement", menuName = "ScriptableObjects/Data/MovementSO")]
public class MovementSO : ScriptableObject
{
    public float speed;
    public float forceJump;
}