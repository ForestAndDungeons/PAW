using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header ("Variables")]
    [SerializeField] bool _isClose;
    [SerializeField] bool _isSecretDoor;
    [SerializeField] GameObject _locket;
    [SerializeField] Collider _collider;
    [SerializeField] RoomEntity _roomEntity;
    
    [Header("Animator")]
    [SerializeField] Animator _animator;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioOpening;
    [SerializeField] AudioClip _audioClosing;
    [SerializeField] AudioClip _audioUnlocking;
    [SerializeField] AudioClip _audioLocked;

    void OnValidate()
    {
        _locket.SetActive(_isSecretDoor);
    }

    void Awake()
    {
        //Iniciamos la funcion Ienumerator en el awake
        StartCoroutine(WaitForOpenFirstTime());
    }

    void OnCollisionEnter(Collision collision)
    {
        //Chequeamos si quien choca la puerta tenga el script de player (sea un player)
        Player pj =  collision.gameObject.GetComponent<Player>();
        //Preguntamos si pj es diferente a null, si es null ningun player esta chocando la puerta
        if (pj!=null)
        {
            //Preguntamos si la puerta es una SecretDoor (Puerta de la habitacion del cofre)
            if (_isSecretDoor)
            {
                //Preguntamos si el player posee una llave, preguntando si el booleano es true
                if (pj._playerBase.haveKey)
                {
                    //Preguntamos si el roomEntity esta desactivado (Que no haya un combate activo)
                    if (_roomEntity == null)
                    {
                        //Preguntamos si la lista de enemigos es <= 0
                        if (_roomEntity._enemyList.Count <= 0)
                        {
                            //Booleano de que el player posee una llave lo ponemos en falso (USAS LA LLAVE)
                            pj._playerBase.SetKey(0);

                            OpenDoor();

                            //Se destruye la puerta
                            //DestroyDoor();
                        }
                    }
                }
                //Si el player no posee una llave
                else
                {
                    //Se reproduce sonido de puerta que no se puede abrir
                    _audioSource.PlayOneShot(_audioLocked);
                    Debug.Log("Esta puerta requiere de una llave para abrise");
                }
            }
        }
    }
    
    //Funcion para abrir puertas
    public void OpenDoor()
    {
        //Desactivamos los GameObjects
        _isClose = false;
        _animator.SetBool("isClose", false);
        _collider.enabled = false;
        //Le subscribimos y desubscribimos las funciones necesarias al EventRoom
        _roomEntity.eventRoom -= OpenDoor;
        _roomEntity.eventRoom += CloseDoor;
        _audioSource.PlayOneShot(_audioOpening);
    }

    //Funcion para cerrar puertas
    public void CloseDoor()
    {
        //Preguntamos si no es una SecretDoor
        if (!_isSecretDoor)
        {
            _isClose = true;
            //Activamos los GameObjects
            _collider.enabled = true;
            _animator.SetBool("isClose", true);
            //Le subscribimos y desubscribimos las funciones necesarias al EventRoom
            _roomEntity.eventRoom -= CloseDoor;
            _roomEntity.eventRoom += OpenDoor;
            _audioSource.PlayOneShot(_audioClosing);
        }
    }

    //Ienumerator que espera 2 segundos para abrir todas las puertas al inicio del juego, excepto las SecretDoors
    IEnumerator WaitForOpenFirstTime()
    {
        yield return new WaitForSeconds(2f);

        if (!_isSecretDoor)
        {
            if (!_isClose)
            {
                OpenDoor();
            }
        }
    }
}