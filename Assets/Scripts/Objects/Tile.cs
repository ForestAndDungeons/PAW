using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hole { None, Shape_ll, Shape_L, Shape_U, Shape_O };

[SelectionBase]
public class Tile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] _parents;
    [SerializeField] TileSkin[] _walls;
    [SerializeField] TileSkin[] _torches;
    [SerializeField] TileSkin _floor;
    [SerializeField] GameObject[] _halfWall;
    [SerializeField] GameObject[] _door;
    [SerializeField] GameObject _blackWall1;
    [SerializeField] GameObject _blackWall2;

    [Space(20)]
    [Header("Wall 1")]
    [SerializeField] [Range(0, 4)] int _wall1Position = 1;

    [SerializeField] [Range(1, 4)] int _wall1Skin = 1;
    [SerializeField] [Range(0, 2)] int _torch1Skin = 0;

    [SerializeField] [Range(0, 4)] int _blackWall1Position = 0;
    [SerializeField] bool _door1;
    [SerializeField] bool _isSecret1;

    [Space(20)]
    [Header("Wall 2")]
    [SerializeField] [Range(0, 4)] int _wall2Position = 2;

    [SerializeField] [Range(1, 4)] int _wall2Skin = 1;
    [SerializeField] [Range(0, 2)] int _torch2Skin = 0;

    [SerializeField] [Range(0, 4)] int _blackWall2Position = 0;
    [SerializeField] bool _door2;
    [SerializeField] bool _isSecret2;

    [Header("Floor")]
    [SerializeField] [Range(0, 4)] int _floorSlider = 1;

    [Header("Half Wall")]
    //[SerializeField] [Range(0, 3)] int _halfWallSlider = 0;
    [SerializeField] Hole _hole;

    void OnValidate()
    {
        if (_door1)
            _wall1Skin = 0;
        if (_door2)
            _wall2Skin = 0;
        if (_wall1Skin == 4)
            _walls[0].GetComponent<BoxCollider>().enabled = false;
        else
            _walls[0].GetComponent<BoxCollider>().enabled = true;
        if (_wall2Skin == 4)
            _walls[1].GetComponent<BoxCollider>().enabled = false;
        else
            _walls[1].GetComponent<BoxCollider>().enabled = true;

        _floor.ChangeSkin(_floorSlider);
        
        _walls[0].ChangeSkin(_wall1Skin);
        _walls[1].ChangeSkin(_wall2Skin);
        _torches[0].ChangeSkin(_torch1Skin);
        _torches[1].ChangeSkin(_torch2Skin);

        ChangeWallPosition(_parents[0], _wall1Position, 2);
        ChangeWallPosition(_parents[1], _wall2Position, 2);
        ChangeWallPosition(_blackWall1.gameObject, _blackWall1Position, -1);
        ChangeWallPosition(_blackWall2.gameObject, _blackWall2Position, -1);

        ChangeHole();
        
        _door[0].SetActive(_door1);
        //_door[0].GetComponent<DoorScript>().OnValidate(_isSecret1);
        _door[1].SetActive(_door2);
        //_door[1].GetComponent<DoorScript>().OnValidate(_isSecret2);
    }

    void ChangeWallPosition(GameObject obj, int slider, float Y)
    {
        switch(slider)
        {
            case 0:
                obj.SetActive(false);
                break;
            case 1:
                obj.SetActive(true);
                obj.transform.localPosition = new Vector3(0f, Y, 2.75f);
                obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                obj.SetActive(true);
                obj.transform.localPosition = new Vector3(2.75f, Y, 0f);
                obj.transform.localRotation = Quaternion.Euler(0, 90, 0);
                break;
            case 3:
                obj.SetActive(true);
                obj.transform.localPosition = new Vector3(0f, Y, -2.75f);
                obj.transform.localRotation = Quaternion.Euler(0, -180, 0);
                break;
            case 4:
                obj.SetActive(true);
                obj.transform.localPosition = new Vector3(-2.75f, Y, 0f);
                obj.transform.localRotation = Quaternion.Euler(0, -90, 0);
                break;
        }
    }

    /*void ChangeBlackWallPosition(GameObject obj, int slider)
    {
        switch (_blackWall2Position)
        {
            case 0:
                _blackWall[1].SetActive(false);
                break;
            case 1:
                _blackWall[1].SetActive(true);
                _blackWall[1].transform.position = new Vector3(0f, -1f, 2.75f);
                _blackWall[1].transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case 2:
                _blackWall[1].SetActive(true);
                _blackWall[1].transform.position = new Vector3(2.75f, -1f, 0f);
                _blackWall[1].transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 3:
                _blackWall[1].SetActive(true);
                _blackWall[1].transform.position = new Vector3(0f, -1f, -2.75f);
                _blackWall[1].transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case 4:
                _blackWall[1].SetActive(true);
                _blackWall[1].transform.position = new Vector3(-2.75f, -1f, 0f);
                _blackWall[1].transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }*/

    void ChangeHole()
    {
        switch (_hole)
        {
            case Hole.None:
                _halfWall[0].SetActive(false);
                _halfWall[1].SetActive(false);
                _halfWall[2].SetActive(false);
                _halfWall[3].SetActive(false);
                break;
            case Hole.Shape_ll:
                _halfWall[0].SetActive(true);
                _halfWall[1].SetActive(false);
                _halfWall[2].SetActive(true);
                _halfWall[3].SetActive(false);
                break;
            case Hole.Shape_L:
                _halfWall[0].SetActive(true);
                _halfWall[1].SetActive(true);
                _halfWall[2].SetActive(false);
                _halfWall[3].SetActive(false);
                break;
            case Hole.Shape_U:
                _halfWall[0].SetActive(true);
                _halfWall[1].SetActive(true);
                _halfWall[2].SetActive(true);
                _halfWall[3].SetActive(false);
                break;
            case Hole.Shape_O:
                _halfWall[0].SetActive(true);
                _halfWall[1].SetActive(true);
                _halfWall[2].SetActive(true);
                _halfWall[3].SetActive(true);
                break;
            default:
                Debug.Log("ERROR");
                break;
        }
    }
}
