using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public float maxLife;
    public float maxSpeed;
    public float attackPower;
    public LayerMask walls;
}
