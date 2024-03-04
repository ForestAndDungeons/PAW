using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/KnockBackSO")]
public class KnockBackSO : ScriptableObject
{
    public float knockBackStrength;
    public float knockBackDuration;
    public Color knockBackColor;
    public Color originalColor;
    public float blinkInterval;
}
