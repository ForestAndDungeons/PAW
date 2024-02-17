using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStats : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] TMP_Text _text;
    string _maxHealth;
    string _attackPower;
    string _speed;
    string _haveKeyText;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _maxHealth = "\nMAX LIFE: " + _player._playerBase.maxHealth;
        _attackPower = "\nATK: " + _player._playerBase.attackPower;
        _speed = "\nSPEED: " + _player._movement.GetSpeed();

        _text.text = "Stats" + _maxHealth + _attackPower + _speed + _haveKeyText;
    }

    private void Update()
    {
        _maxHealth = "\nMAX LIFE: " + _player._playerBase.maxHealth;
        _attackPower = "\nATK: " + _player._playerBase.attackPower;
        _speed = "\nSPEED: " + _player._movement.GetSpeed();

        if (_player._playerBase.haveAKey)
        {
            _haveKeyText = "\nHAVE KEY";
        }
        else
        {
            _haveKeyText = "\nNO KEY";
        }

        _text.text= "STATS\n" + _maxHealth + _attackPower + _speed + _haveKeyText;
    }
}