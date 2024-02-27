using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hole { None, Shape_ll, Shape_L, Shape_U, Shape_O };

[SelectionBase]
public class Tile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TileSkin _wall1;
    [SerializeField] TileSkin _torch1;
    [SerializeField] TileSkin _wall2;
    [SerializeField] TileSkin _torch2;
    [SerializeField] TileSkin _floor;
    [SerializeField] GameObject[] _halfWall;
    [SerializeField] GameObject[] _door;
    [SerializeField] GameObject[] _blackWall;

    [Space(20)]
    [Header("Wall 1")]
    
    [SerializeField] [Range(0, 3)] int _wall1Slider = 1;
    [SerializeField] [Range(0, 2)] int _torch1Slider = 0;
    [SerializeField] bool _door1;
    [SerializeField] bool _isSecret1;
    [SerializeField] bool _blackWall1;

    [Space(20)]
    [Header("Wall 2")]
    [SerializeField] [Range(0, 3)] int _wall2Slider = 0;
    [SerializeField] [Range(0, 2)] int _torch2Slider = 0;
    [SerializeField] bool _door2;
    [SerializeField] bool _isSecret2;
    [SerializeField] bool _blackWall2;

    [Header("Floor")]
    [SerializeField] [Range(0, 4)] int _floorSlider = 1;

    [Header("Half Wall")]
    //[SerializeField] [Range(0, 3)] int _halfWallSlider = 0;
    [SerializeField] Hole _hole;

    void OnValidate()
    {        
        if (_door1 || _blackWall1)
        {
            _wall1Slider = 0;
            if (_door1)
                _blackWall1 = false;
            else
                _door1 = false;
        }
        if (_door2 || _blackWall2)
        {
            _wall2Slider = 0;
        if (_door2)
            _blackWall2 = false;
        else
            _door2 = false;
        }

        _wall1.ChangeSkin(_wall1Slider);
        _torch1.ChangeSkin(_torch1Slider);

        _wall2.ChangeSkin(_wall2Slider);
        _torch2.ChangeSkin(_torch2Slider);
        _floor.ChangeSkin(_floorSlider);

        ChangeHole();
        
        _door[0].SetActive(_door1);
        _door[0].GetComponent<DoorScript>().OnValidate(_isSecret1);
        _door[1].SetActive(_door2);
        _door[1].GetComponent<DoorScript>().OnValidate(_isSecret2);

        _blackWall[0].SetActive(_blackWall1);
        _blackWall[1].SetActive(_blackWall2);
    }

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
