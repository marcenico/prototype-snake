using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  public GameObject itemPrefab;
  public Transform[] spawnPoints;

  public void SpawnItem()
  {
    if (itemPrefab == null || spawnPoints.Length == 0)
    {
      Debug.LogWarning("Item prefab or spawn points are not set.");
      return;
    }

    // Choose a random spawn point
    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

    GameObject newItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
    Item itemComponent = newItem.GetComponent<Item>();
    itemComponent.OnCollected += HandleItemCollected;
  }

  private void HandleItemCollected(Item collectedItem)
  {
    collectedItem.OnCollected -= HandleItemCollected; // Detach the event handler
    Destroy(collectedItem.gameObject); // Destroy the collected item

    // Spawn a new item after the previous one is collected
    SpawnItem();
  }
}
