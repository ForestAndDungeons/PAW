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

    public void Anim_event_BossPlayJumpSound()
    {
        _boss.SoundJump();
    }

    public void Anim_event_BossPlayFireballSound()
    {
        _boss.SoundFireball();
    }
}
