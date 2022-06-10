using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isSecretDoor = false;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;
    [SerializeField] RoomEntity _roomEntity;
    //public RoomEntity roomEntity;
    

    private void Start()
    {
       // roomEntity.eventRoom += OpenDoor;
    }
    private void Update()
    {
        /*if (roomEntity._playerList.Count > 0)
        {
            roomEntity.eventRoom += CloseDoor;
            Debug.Log("Tengo que cerrar las puertas prro");
            if (roomEntity._enemyList.Count == 0)
            {
                roomEntity.eventRoom += OpenDoor;
                Debug.Log("Tengo que abrir las puertas prro");
            }
        }*/
    }

    private void Awake()
    {
        StartCoroutine(WaitForOpenFirstTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player pj =  collision.gameObject.GetComponent<Player>();
        if (pj!=null)
        {
            if (isSecretDoor)
            {
                if (pj._playerBase.KeyGetter())
                {
                    if(_roomEntity != null)
                    { 
                        if (_roomEntity._enemyList.Count <= 0)
                        {
                            pj._playerBase.KeySetter(false);
                            _audioSource.PlayOneShot(_audioClip[1]);
                            DestroyDoor();
                        }
                    }
                    else
                    {
                        pj._playerBase.KeySetter(false);
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
        this.gameObject.SetActive(false);
       /* roomEntity.eventRoom -= CloseDoor;
        roomEntity.eventRoom += OpenDoor;*/
    }

    public void CloseDoor()
    {
        this.gameObject.SetActive(true);
        /*  roomEntity.eventRoom -= OpenDoor;
          roomEntity.eventRoom += CloseDoor;*/
    }

    public void DestroyDoor()
    {
        Destroy(this.gameObject, 0.7f);
    }
    IEnumerator WaitForOpenFirstTime()
    {
        yield return new WaitForSeconds(2f);

        if (!isSecretDoor)
        {
            OpenDoor();
        }
    }
}
