using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Juan Barrientos Picasso - P.A.W
public class DoorScript : MonoBehaviour
{
    [SerializeField] bool _isSecretDoor;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;
    [SerializeField] RoomEntity _roomEntity;
    [SerializeField] bool _isOpen;

    private void Awake()
    {
        //Iniciamos la funcion Ienumerator en el awake
        StartCoroutine(WaitForOpenFirstTime());
    }

    private void OnCollisionEnter(Collision collision)
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
                if (pj._playerBase.GetKey())
                {
                    //Preguntamos si el roomEntity esta desactivado (Que no haya un combate activo)
                    if (_roomEntity == null)
                    {
                        //Preguntamos si la lista de enemigos es <= 0
                        if (_roomEntity._enemyList.Count <= 0)
                        {
                            //Booleano de que el player posee una llave lo ponemos en falso (USAS LA LLAVE)
                            pj._playerBase.SetKey(false);
                            //Se reproduce el audio de que se abre la puerta
                            _audioSource.PlayOneShot(_audioClip[1]);
                            //Se destruye la puerta
                            DestroyDoor();
                        }
                    }
                }
                //Si el player no posee una llave
                else
                {
                    //Se reproduce sonido de puerta que no se puede abrir
                    _audioSource.PlayOneShot(_audioClip[0]);
                    Debug.Log("Esta puerta requiere de una llave para abrise");
                }
            }
        }
    }
    
    //Funcion para abrir puertas
    public void OpenDoor()
    {
        //Preguntamos si no es una SecretDoor
        if (!_isSecretDoor)
        {
            //Desactivamos los GameObjects
            Debug.Log("Abro Puertas");
            _isOpen = true;
            this.gameObject.SetActive(false);
            //Le subscribimos y desubscribimos las funciones necesarias al EventRoom
            _roomEntity.eventRoom -= OpenDoor;
            _roomEntity.eventRoom += CloseDoor;
        }
    }

    //Funcion para cerrar puertas
    public void CloseDoor()
    {
        //Preguntamos si no es una SecretDoor
        if (!_isSecretDoor)
        {
            _isOpen = false;
            Debug.Log("Cierro Puertas");
            //Activamos los GameObjects
            this.gameObject.SetActive(true);
            //Le subscribimos y desubscribimos las funciones necesarias al EventRoom
            _roomEntity.eventRoom -= CloseDoor;
            _roomEntity.eventRoom += OpenDoor;
        }
    }

    //Funcion para destruir puerta
    public void DestroyDoor()
    {
        Destroy(this.gameObject, 0.7f);
    }
    //Ienumerator que espera 2 segundos para abrir todas las puertas al inicio del juego, excepto las SecretDoors
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
