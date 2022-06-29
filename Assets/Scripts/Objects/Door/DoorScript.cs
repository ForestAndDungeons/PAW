using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] bool _isSecretDoor;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;
    [SerializeField] RoomEntity _roomEntity;
    [SerializeField] bool _isOpen;
   

    private void Awake()
    {
        StartCoroutine(WaitForOpenFirstTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player pj =  collision.gameObject.GetComponent<Player>();
        if (pj!=null)
        {
            if (_isSecretDoor)
            {
                if (pj._playerBase.GetKey())
                {
                    if(_roomEntity != null)
                    { 
                        if (_roomEntity._enemyList.Count <= 0)
                        {
                            pj._playerBase.SetKey(false);
                            _audioSource.PlayOneShot(_audioClip[1]);
                            DestroyDoor();
                        }
                    }
                    else
                    {
                        pj._playerBase.SetKey(false);
                        _audioSource.PlayOneShot(_audioClip[1]);
                        DestroyDoor();
                    }
                }
                else
                {
                    _audioSource.PlayOneShot(_audioClip[0]);
                    Debug.Log("Esta puerta requiere de una llave para abrise");
                }
            }
        }

    }
    public void OpenDoor()
    {
        if (!_isSecretDoor)
        {
            Debug.Log("Abro Puertas");
            _isOpen = true;
            this.gameObject.SetActive(false);
            _roomEntity.eventRoom -= OpenDoor;
            _roomEntity.eventRoom += CloseDoor;
        }
    }

    public void CloseDoor()
    {
        if (!_isSecretDoor)
        {
            _isOpen = false;
            Debug.Log("Cierro Puertas");
            this.gameObject.SetActive(true);
            _roomEntity.eventRoom -= CloseDoor;
            _roomEntity.eventRoom += OpenDoor;
        }
    }

    public void DestroyDoor()
    {
        Destroy(this.gameObject, 0.7f);
    }
    IEnumerator WaitForOpenFirstTime()
    {
        yield return new WaitForSeconds(2f);

        if (!_isSecretDoor)
        {
            if (!_isOpen)
            {
                OpenDoor();
            }
        }
    }
}
