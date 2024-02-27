using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    Light testLight;

    void Start()
    {
        testLight = GetComponent<Light>();
        //testLight.intensity
        //StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    { 
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            testLight.enabled = !testLight.enabled;
        }
    }
}
