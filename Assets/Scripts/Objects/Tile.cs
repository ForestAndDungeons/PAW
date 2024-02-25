using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hole { None, Shape_ll, Shape_L, Shape_U, Shape_O };

public class Tile : MonoBehaviour
{
    [SerializeField] TileSkin _wall1;
    [SerializeField] TileSkin _wall2;
    [SerializeField] TileSkin _floor;
    [SerializeField] GameObject[] _halfWall;
    [SerializeField] GameObject[] _door;
    [SerializeField] GameObject[] _blackWall;

    [Header("Wall 1")]
    [SerializeField] [Range(0, 3)] int _wall1Slider = 1;
    [SerializeField] bool _hasTorch1;
    [SerializeField] bool _door1;
    [SerializeField] bool _blackWall1;

    [Header("Wall 2")]
    [SerializeField] [Range(0, 3)] int _wall2Slider = 0;
    [SerializeField] bool _hasTorch2;
    [SerializeField] bool _door2;
    [SerializeField] bool _blackWall2;

    [Header("Floor")]
    [SerializeField] [Range(0, 4)] int _floorSlider = 1;

    [Header("HalfWalls")]
    //[SerializeField] [Range(0, 3)] int _halfWallSlider = 0;
    [SerializeField] Hole _hole;

    void OnValidate()
    {
        if (_door1 || _blackWall1)
            _wall1Slider = 0;
        if (_door2 || _blackWall2)
            _wall2Slider = 0;

        _wall1.ChangeSkin(_wall1Slider, _hasTorch1);
        _wall2.ChangeSkin(_wall2Slider, _hasTorch2);
        _floor.ChangeSkin(_floorSlider, false);

        ChangeHole();
        
        _door[0].SetActive(_door1);
        _door[1].SetActive(_door2);

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
