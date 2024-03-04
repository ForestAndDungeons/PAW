using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{

    public void OnDestroy()
    {
        Destroy(this.gameObject, 0.5f);
    }
}
