using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    public void OnStart();
    public void OnUpdate();
    public void OnExit();
}
