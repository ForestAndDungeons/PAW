using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    GameObject _player;
    public GameObject player { get { return _player; } }

    Cam _otherCam;
    bool check;
    bool isInOther;
    public List<ObjectFader> _faders;

    [SerializeField] bool _player1Camera;
    [SerializeField] LayerMask IgnoreLayer;

    void Start()
    {
        foreach(Player player in GameManager.Instance._players)
        {
            if (_player1Camera && player.isPlayer1)
            {
                _player = player.gameObject;
                player.myCamera = this.GetComponent<Camera>();
                _otherCam = player.otherPlayer.myCamera.GetComponent<Cam>();
            }
            else if(!_player1Camera && !player.isPlayer1)
            {
                _player = player.gameObject;
                player.myCamera = this.GetComponent<Camera>();
                _otherCam = player.otherPlayer.myCamera.GetComponent<Cam>();
            }
        }
    }

    void Update()
    {
        if (_player != null)
        {
            Vector3 dir = _player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            Debug.DrawRay(transform.position, dir, Color.red, 2f);

            if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreLayer))
            {
                //Debug.Log(hit.collider.name);
                if (hit.collider == null)
                    return;

                if (hit.collider.gameObject == _player)
                {
                    //Nothing is in front of the player
                    if (_faders != null)
                    {
                        foreach (ObjectFader fader in _faders)
                        {
                            foreach(ObjectFader fader2 in _otherCam._faders)
                            {
                                if (fader == fader2)
                                    isInOther = true;
                            }
                            if (isInOther == false)
                                fader.doFade = false;
                            else
                                isInOther = false;
                        }
                        _faders.Clear();
                    }
                }
                else
                {
                    foreach(ObjectFader fader in _faders)
                    {
                        if(fader == hit.collider.gameObject.GetComponent<ObjectFader>())
                        {
                            check = true;
                        }

                    }
                    if (check == false)
                    {
                        /*foreach (ObjectFader fader in _faders)
                        {
                            fader.doFade = false;
                            _faders.Clear();
                        }*/
                        if (hit.collider.gameObject.GetComponent<ObjectFader>())
                        {
                            _faders.Add(hit.collider.gameObject.GetComponent<ObjectFader>());
                            foreach (ObjectFader fader in _faders)
                                fader.doFade = true;
                        }
                    }
                    else
                        check = false;
                }
            }
        }
    }
}


/* Original
public class Cam : MonoBehaviour
{
    [SerializeField] ObjectFader _fader;
    [SerializeField] GameObject _player;
    [SerializeField] LayerMask IgnoreLayer;

    void Update()
    {
        if (_player != null)
        {
            Vector3 dir = _player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            Debug.DrawRay(transform.position, dir, Color.red, 2f);
            
            if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreLayer))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider == null)
                    return;

                if (hit.collider.gameObject == _player)
                {
                    //Nothing is in front of the player
                    if (_fader != null)
                    {
                        _fader.doFade = false;
                        //_fader = null;
                    }
                }
                else
                {
                    if(_fader != null)
                        _fader.doFade = false;
                    _fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if (_fader != null)
                    {
                        _fader.doFade = true;
                    }
                }
            }
        } 
    }
}
*/