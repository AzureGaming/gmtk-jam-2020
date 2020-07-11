using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
  public GameObject enemy;
  public Camera cam;
  public Transform player;

  public int spawnDelay = 1;

  public void StartSpawning()
  {
    StartCoroutine(SpawnEnemies());
  }

  public void StopSpawning()
  {
    StopCoroutine(SpawnEnemies());
  }

  IEnumerator SpawnEnemies()
  {
    while (true)
    {
      // Vector3 enemyPosition = new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0f);
      float randomPosOnCamX = Random.Range(cam.ViewportToWorldPoint(new Vector3(0, 0)).x, cam.ViewportToWorldPoint(new Vector3(1, 0)).x);
      float randomPosOnCamY = Random.Range(cam.ViewportToWorldPoint(new Vector3(0, 0)).y, cam.ViewportToWorldPoint(new Vector3(1, 1)).y);
      Vector3 enemyPosition = new Vector3(randomPosOnCamX, randomPosOnCamY, 0f);
      if ((enemyPosition - player.transform.position).magnitude < 3)
      {
        continue;
      }
      else
      {
        Instantiate(enemy, enemyPosition, Quaternion.identity);
      }
      yield return new WaitForSeconds(spawnDelay);
    }
  }
}
