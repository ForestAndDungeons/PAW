using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    /*[SerializeField] AudioSource _myAudioSource;
    [SerializeField] AudioClip[] _audioClips;
    [SerializeField] Animation _transition;*/

    void Update()
    {
        /* if (eventRoom != null)
         {
             eventRoom();
         }*/
        /*if (_playerList.Count > 0)
        {
            for (int i = 0; i < _doorList.Count; i++)
            {
                _doorList[i].CloseDoor();
                _transition.SetTrigger("fadeIn");
                _myAudioSource.PlayOneShot(_audioClips[1]);
            }

            if (_enemyList.Count == 0)
            {
                for (int i = 0; i < _doorList.Count; i++)
                {
                    if (!_doorList[i].isSecretDoor)
                    {
                        _doorList[i].OpenDoor();
                        _transition.SetTrigger("fadeIn");
                        _myAudioSource.PlayOneShot(_audioClips[0]);
                    }
                }
            }
        }*/
    }
}
