using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  public GameObject itemPrefab;
  public Transform[] spawnPoints;
  public float spawnInterval = 5f;

  private float timer = 0f;

  private void Start()
  {
    timer = spawnInterval;
  }

  private void Update()
  {
    // Count down the timer
    timer -= Time.deltaTime;

    // Spawn an item if the timer reaches zero
    if (timer <= 0f)
    {
      SpawnItem();
      timer = spawnInterval;
    }
  }

  private void SpawnItem()
  {
    if (itemPrefab == null || spawnPoints.Length == 0)
    {
      Debug.LogWarning("Item prefab or spawn points are not set.");
      return;
    }

    // Choose a random spawn point
    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

    // Instantiate the item at the chosen spawn point
    GameObject newItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
    // Optionally, you can add more logic here to set item properties

    Debug.Log("Item spawned at " + spawnPoint.position);
  }
}
