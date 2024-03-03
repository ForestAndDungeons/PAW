using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerPos : MonoBehaviour
{
    void Update()
    {
        transform.position = (GameManager.Instance._players[0].transform.position + GameManager.Instance._players[1].transform.position) / 2.0f;
    }
}
