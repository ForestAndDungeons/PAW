using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] RoomEntity _roomEntity;
    public RoomEntity roomEntity { get { return _roomEntity; } set { _roomEntity = value; } }

    [Header ("Variables")]
    [SerializeField] bool _isOpen = true;
    [SerializeField] bool _isSecretDoor;
    [SerializeField] GameObject _locket;
    [SerializeField] Collider _collider;
    
    [Header("Animator")]
    [SerializeField] Animator _animator;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioOpening;
    [SerializeField] AudioClip _audioClosing;
    [SerializeField] AudioClip _audioUnlocking;
    [SerializeField] AudioClip _audioLocked;

    void Awake()
    {
        IsSecretDoor(_isSecretDoor);
        //Iniciamos la funcion Ienumerator en el awake
        StartCoroutine(Wait());
    }

    /*public void OnValidate(bool value)
    {
        IsSecretDoor(value);
    }*/

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
                if (pj._playerBase.keysCollected > 0)
                {
                    //Preguntamos si el roomEntity esta desactivado (Que no haya un combate activo)
                    if (roomEntity != null)
                    {
                        //Preguntamos si la lista de enemigos es <= 0
                        if (roomEntity._enemyList.Count <= 0)
                        {
                            //Booleano de que el player posee una llave la restamos (USAS LA LLAVE)
                            pj._playerBase.keysCollected--;
                            IsSecretDoor(false);
                            StartCoroutine(Wait());
                        }
                    }
                    else
                    {
                        pj._playerBase.keysCollected--;
                        IsSecretDoor(false);
                        StartCoroutine(Wait());
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

    public void IsSecretDoor(bool value)
    {
        _isSecretDoor = value;
        _locket.SetActive(_isSecretDoor);
    }

    //Funcion para abrir puertas
    public void OpenDoor()
    {
        if(!_isSecretDoor)
        {
            //Desactivamos los GameObjects
            _isOpen = true;
            _animator.SetBool("isOpen", true);
            _collider.enabled = false;

            //Le subscribimos y desubscribimos las funciones necesarias al EventRoom
            roomEntity.eventRoom -= OpenDoor;
            roomEntity.eventRoom += CloseDoor;
            _audioSource.PlayOneShot(_audioOpening);
        }
    }

    //Funcion para cerrar puertas
    public void CloseDoor()
    {
        _isOpen = false;
        //Activamos los GameObjects
        _collider.enabled = true;
        _animator.SetBool("isOpen", false);

        //Le subscribimos y desubscribimos las funciones necesarias al EventRoom
        roomEntity.eventRoom += OpenDoor;
        roomEntity.eventRoom -= CloseDoor;
        _audioSource.PlayOneShot(_audioClosing);
    }

    //Ienumerator que espera 2 segundos para abrir todas las puertas al inicio del juego, excepto las SecretDoors
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        if (_isOpen)
        {
            OpenDoor();
        }
        else
            CloseDoor();
    }
}