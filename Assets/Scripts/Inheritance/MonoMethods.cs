using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoMethods : MonoBehaviour
{
    public void DestroyThisObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy.gameObject);
    }
}
