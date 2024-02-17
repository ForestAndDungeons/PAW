using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport
{
    Player _player;

    public Teleport(Player player)
    {
        _player = player;
    }

    public void TeleportToRoom(List<GameObject> playerList)
    {
        if (playerList.Count > 0)
        {
            foreach(GameObject playerObject in playerList)
            {
                if(playerObject != null)
                {
                    if (playerObject != _player)
                    {
                        playerObject.GetComponent<Player>().otherPlayer.transform.position = playerObject.transform.position;
                    }
                }
            }
        }
    }
}
