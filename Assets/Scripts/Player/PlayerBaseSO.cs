using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataPlayerBase", menuName = "ScriptableObjects/Data/PlayerBase")]
public class PlayerBaseSO : ScriptableObject
{
    public float maxHealth;
    public float attackDamage;
    public float attackSpeed;
    public float movementSpeed;
    public float jumpForce;
    public float immuneTime;
}