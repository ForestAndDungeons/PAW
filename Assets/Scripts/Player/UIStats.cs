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
    string _keyCollectedText;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _maxHealth = "\nMAX LIFE: " + _player._playerBase.maxHealth;

        _attackPower = "\nATK: " + _player._playerBase.attackPower;
        _speed = "\nSPEED: " + _player._movement.speed;

        _text.text = "Stats" + _maxHealth + _attackPower + _speed + _keyCollectedText;
    }

    private void Update()
    {
        _maxHealth = "\nMAX LIFE: " + _player._playerBase.maxHealth;
        _attackPower = "\nATK: " + _player._playerBase.attackPower;
        _speed = "\nSPEED: " + _player._movement.speed;

        _keyCollectedText = "\nKEYS: " + _player._playerBase.keysCollected;

        _text.text= "STATS\n" + _maxHealth + _attackPower + _speed + _keyCollectedText;
    }
}