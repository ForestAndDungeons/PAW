using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawner : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] Fireball _fireball;
    [Header("Spawner Position")]
    [SerializeField] Vector3 _spawnCenter;
    [SerializeField] Vector3 _spawnSize;

    public void SpawnFireBall()
    {
        Vector3 pos = (this.transform.position + _spawnCenter) + new Vector3(Random.Range(-_spawnSize.x / 2, _spawnSize.x / 2), Random.Range(-_spawnSize.y / 2, _spawnSize.y / 2), Random.Range(-_spawnSize.z / 2, _spawnSize.z / 2));

        _fireball.InstantiateFireball(pos,this.transform);

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(this.transform.position + _spawnCenter, _spawnSize);
    }
}
