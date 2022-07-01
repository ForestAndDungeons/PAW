using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Player _otherPlayer;

    public void TeleportToRoom(List<GameObject> playerList)
    {
        Debug.Log("Entre teleport");
        if (playerList.Count > 0)
        {
            foreach(GameObject room in playerList)
            {
                if(room != null)
                {
                    if (room == this.gameObject){}
                    else
                    {
                        this.transform.position = _otherPlayer.transform.position;
                    }
                }
            }
        }
    }
}
