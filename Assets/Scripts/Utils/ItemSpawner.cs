using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  public GameObject itemPrefab;

  [SerializeField] private float spawnDelay = 1f;

  private Vector2 minWorldPos;
  private Vector2 maxWorldPos;
  private readonly float screenMargin = 2f;

  private void Start()
  {
    SetScreenBoundaries();
  }

  public void SpawnItem()
  {
    if (itemPrefab == null)
    {
      Debug.LogWarning("Item prefab is not set.");
      return;
    }

    Vector2 spawnPosition = GetRandomVector2Pos();

    GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    Item itemComponent = newItem.GetComponent<Item>();
    itemComponent.OnCollected += HandleItemCollected;
  }

  private Vector2 GetRandomVector2Pos()
  {
    // Generate a random position within the screen boundaries
    return new Vector2(Random.Range(minWorldPos.x, maxWorldPos.x), Random.Range(minWorldPos.y, maxWorldPos.y));
  }

  private void SetScreenBoundaries()
  {
    // Get the screen boundaries in world coordinates
    minWorldPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
    maxWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

    // Apply the margin to the screen boundaries
    minWorldPos.x += screenMargin;
    maxWorldPos.x -= screenMargin;
    minWorldPos.y += screenMargin;
    maxWorldPos.y -= screenMargin;
  }

  private void HandleItemCollected(Item collectedItem)
  {
    collectedItem.OnCollected -= HandleItemCollected; // Detach the event handler
    Destroy(collectedItem.gameObject); // Destroy the collected item

    // Spawn a new item after the previous one is collected with a delay
    Invoke(nameof(SpawnItem), spawnDelay);
  }
}
