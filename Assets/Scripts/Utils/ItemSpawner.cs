using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  public GameObject itemPrefab;
  public Transform[] spawnPoints;

  private void Start()
  {
    SpawnItem();
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

    GameObject newItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
    newItem.GetComponent<Item>().OnCollected += HandleItemCollected;
  }

  private void HandleItemCollected(Item previousItem)
  {
    // Instantiate the item at the chosen spawn point
    SpawnItem();
    // Destroy the previous item
    Destroy(previousItem.gameObject);
  }
}
