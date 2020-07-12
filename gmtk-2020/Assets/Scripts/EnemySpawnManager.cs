using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawnManager : MonoBehaviour
{
  public GameObject enemy;
  public GameObject bunny;
  public Transform player;
  public Tilemap tilemap;
  public GameManager gameManager;

  public int spawnDelay = 1;

  Vector3 worldMin;
  Vector3 worldMax;

  private void Start()
  {
    LoadBoundaries();
  }

  public void StartSpawning()
  {
    StartCoroutine(SpawnEnemies());
  }

  public void CleanUpSpawns()
  {
    foreach (Enemy spawn in FindObjectsOfType<Enemy>())
    {
      Destroy(spawn.gameObject);
    }
  }

  public void StopSpawning()
  {
    StopAllCoroutines();
  }

  IEnumerator SpawnEnemies()
  {
    while (true && !gameManager.IsGloryKilling())
    {
      SpawnEnemy();
      yield return new WaitForSeconds(spawnDelay);
    }
  }

  void SpawnEnemy()
  {
    if (!player)
    {
      player = FindObjectOfType<ChainsawController>().transform;
    }
    float randomEnemy = Random.Range(0f, 100f);
    GameObject enemyType = randomEnemy >= 25 ? enemy : bunny;

    float randomPosOnCamX = Random.Range(Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x);
    float randomPosOnCamY = Random.Range(Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y, Camera.main.ViewportToWorldPoint(new Vector3(1, 1)).y);
    Vector3 enemyPosition = new Vector3(randomPosOnCamX, randomPosOnCamY, 0f);
    if ((enemyPosition - player.transform.position).magnitude >= 3 && IsWithinBoundaries(enemyPosition))
    {
      Instantiate(enemyType, enemyPosition, Quaternion.identity);
    }
  }

  void LoadBoundaries()
  {
    tilemap.CompressBounds();
    worldMin = tilemap.transform.TransformPoint(tilemap.localBounds.min); // bottom left
    worldMax = tilemap.transform.TransformPoint(tilemap.localBounds.max); // top right
  }

  bool IsWithinBoundaries(Vector3 pos)
  {
    if (pos.x >= worldMin.x && pos.x <= worldMax.x)
    {
      if (pos.y >= worldMin.y && pos.y <= worldMax.y)
      {
        return true;
      }
    }
    return false;
  }
}
