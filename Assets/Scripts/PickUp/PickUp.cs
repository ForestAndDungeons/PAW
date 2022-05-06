using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    protected PickUpSound _pickUpSound;
    public abstract void Pick(PlayerBase playerBase);

}
