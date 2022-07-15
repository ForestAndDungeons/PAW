using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEvent : MonoBehaviour
{
    [SerializeField] Boss _boss;

    public void Anim_event_BossStopHit()
    {
        Debug.Log("Entro Anim_event_BossStopHit");
        _boss.StopHit();
    }
}
