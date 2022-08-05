using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Juan Barrientos Picasso - P.A.W
public class RoomEntity : MonoBehaviour
{
    //Lista de teleports para los players
    [SerializeField] List<Teleport> _teleport;
    [Header("BossRoom")]
    [SerializeField] bool _isBossRoom;

    [SerializeField] GameObject _bossLifeSlider;

    [Header("EnemyWithKey")]
    //Variable privada para guardar un enemigo random
    [SerializeField] int _randomEnemy;

    [Header("Combat Music")]
    //Audios de la musica
    SceneTransition _sceneTransition;
    AudioSource _musicAudioSource;
    Animator _musicTransition;
    bool _musicFlag;
    [SerializeField] AudioClip[] _audioClips;

    [Header("Audio Door")]
    [SerializeField] AudioSource _audioSourceDoor;
    [SerializeField] AudioClip[] _doorsClips;

    [Header("Listas")]
    //Lista para guardar enemigos de la habitacion
    [SerializeField] public List<GameObject> _enemyList = new List<GameObject>();
    //Lista para guardar players que esten en la habitacion
    [SerializeField] public List<GameObject> _playerList = new List<GameObject>();

    //Creamos el delegate y event
    public delegate void EventRoom();
    public event EventRoom eventRoom;
    private void Start()
    {
        //Co-Rutina que inicia la funcion WaitForFillList
        StartCoroutine(WaitForFillList());
        _sceneTransition = FindObjectOfType<SceneTransition>();
        _musicAudioSource = _sceneTransition.GetComponent<AudioSource>();
        _musicTransition = _sceneTransition.GetComponent<Animator>();
    }

    //Funcion OnTrigger que chequea quien entra dentro del area
    private void OnTriggerEnter(Collider other)
    {
        //Chequeamos si colisiono con un Player
        Player pj = other.gameObject.GetComponent<Player>();
        
        if(pj != null)
        {
            //Ejecutamos la funcion para agregar los players a la lista de players dentro del RoomEntity
            AddPlayer(other.gameObject);
            _musicTransition.SetTrigger("fadeOut");
            StartCoroutine(WaitForMusicTransition(2));
            _musicFlag = true;
        }
        //Chequeamos si colisiono con un Enemigo
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy)
        {
            //Se llena la lista con enemigos
            _enemyList.Add(other.gameObject);

        }
        else { Debug.Log("."); }
    }

    //Mientras el jugador siga dentro del RoomEntity pregunta
    private void OnTriggerStay(Collider other)
    {
        //Si la lista de enemigos es <= 0 y la musica de combate esta Ons
        if (_enemyList.Count <= 0 && _musicFlag)
        {
            //Realiza un fadeout a la musica de combate
            _musicTransition.SetTrigger("fadeOut");
            //Inicia el Ienumerator para cambiar de musica
            StartCoroutine(WaitForMusicTransition(0));
            //Seteamos el booleano de musica de combate a falso
            _musicFlag = false;
        }
    }

    //Funcion que elimina enemigos de la lista y cuando no tenga mas enemigos ejecuta el eventRoom que ejecutaria la funcion OpenDoor
    public void ElimEnemyInList(GameObject enemy)
    {
        _enemyList.Remove(enemy);
        if (_enemyList.Count <=0)
        {
            if (eventRoom != null)
            {
                _audioSourceDoor.PlayOneShot(_doorsClips[0]);
                eventRoom();
            }
            DestroyRoomEntity();
        }
    }
    //Funcion que le otorga un drop de llave a un enemigo de manera random
    public void EnemyWithKey(int enemyRandom)
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            int index = _enemyList.IndexOf(_enemyList[i]);
            if (enemyRandom == index)
            {
                Enemy enemyWithKey = _enemyList[i].GetComponent<Enemy>();
                enemyWithKey._enemyBase.EnemyWithKey(true);
                
            }
        }
    }

    public void DestroyRoomEntity()
    {
        //Destruye el RoomEntity de la habitacion
        Destroy(this.gameObject, 2.5f);
    }

    //Contador con Ienumerators
    IEnumerator WaitForFillList()
    {
        //Espera 5 milesimas para tomar la lista de enemigos, sino estaria vacia
        yield return new WaitForSeconds(0.5f);
        //Asignamos al int randonEnemy un enemigo random de la lista de enemigos
        _randomEnemy = Random.Range(0, _enemyList.Count);
        //Le mandamos a la funcion EnemyWithKey el enemigo elegido para que se le de el drop de llave
        EnemyWithKey(_randomEnemy);
    }
    public void AddPlayer(GameObject playerGO)
    {
        
        //playerGO es Player GameObject
        _playerList.Add(playerGO);

        //Recorre la lista de teleports y teletransporta al otro player que no esta en PlayersList a la posicion del primero que entro a la habitacion
        if (_teleport!=null)
        {
            foreach (Teleport tp in _teleport)
            {
                tp.TeleportToRoom(_playerList);
            }
        }
        //Inicia el combate de la habitacion, ejecutamos el eventRoom que usa el CloseDoors
        if (_playerList.Count <= 1)
        {
            if (eventRoom != null)
            {
                eventRoom();
                StartCoroutine(WaitForDoorSound());
            }
        }
        if (_playerList.Count >= 2)
        {
            if (_isBossRoom)
            {
                _bossLifeSlider.SetActive(true);
                var boxCollider = this.gameObject.GetComponent<Collider>();
                if (boxCollider !=null)
                {
                    boxCollider.enabled = false;
                    Debug.Log("PlayerList : " + _playerList.Count);
                }

            }
        }
    }
    IEnumerator WaitForDoorSound()
    {
       
        yield return new WaitForSeconds(0.6f);
        _audioSourceDoor.PlayOneShot(_doorsClips[1]);
    }

    IEnumerator WaitForMusicTransition(int clipIndex)
    {
        //Espera 2 segundos para intercambiar entre los diferentes AudioSource, con su respectiva musica
        yield return new WaitForSeconds(2f);
        _musicAudioSource.Stop();
        _musicAudioSource.volume = 0f;
        _musicAudioSource.clip = _audioClips[clipIndex];
        _musicAudioSource.Play();
    }

    //PlayerList es privada, para no hacerla publica y poder usar la lista de manera actualizada usamos el Getter
    public List<GameObject> GetPlayerList()
    {
        return _playerList;
    }
}