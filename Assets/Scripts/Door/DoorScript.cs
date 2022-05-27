using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    
    private void Awake()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        this.gameObject.SetActive(false);
    }

    public void CloseDoor()
    {
        this.gameObject.SetActive(true);
    }
}
