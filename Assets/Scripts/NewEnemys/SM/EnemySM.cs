using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : MonoBehaviour
{
    IState _currentState;
    Dictionary<EnemyStates, IState> _allEnemyStates = new Dictionary<EnemyStates, IState>();


    public void Update()
    {
        _currentState.OnUpdate();
    }

    public void EnemyChangeStates(EnemyStates enemyStates)
    {
        if (!_allEnemyStates.ContainsKey(enemyStates)) { return; }

        if (_currentState != null) { _currentState.OnExit(); }
        _currentState = _allEnemyStates[enemyStates];
        _currentState.OnStart();
    }

    public void AddEnemyStates(EnemyStates enemyStateName, IState enemyStateAction)
    {
        if (!_allEnemyStates.ContainsKey(enemyStateName)) { _allEnemyStates.Add(enemyStateName, enemyStateAction); }
        else { _allEnemyStates[enemyStateName] = enemyStateAction; }
    }
}
