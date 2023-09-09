using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  [SerializeField] private float spawnDelay = 0.5f;

  private ObjectPool objectPool;
  private Vector2 minWorldPos;
  private Vector2 maxWorldPos;
  private readonly float screenMargin = 2f;

  private void Start()
  {
    SetScreenBoundaries();
    objectPool = GetComponent<ObjectPool>(); // Get the ObjectPool component on the same GameObject
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

  private void SpawnItemWithDelay()
  {
    Invoke(nameof(SpawnItem), spawnDelay);
  }

  public void SpawnItem()
  {
    GameObject newItem = objectPool.GetObject(); // Use the object pool to get an item
    if (newItem == null)
    {
      Debug.LogWarning("Item pool is empty. Make sure the ObjectPool component is attached and configured.");
      return;
    }

    Vector2 spawnPosition = GetRandomScreenPos();
    newItem.transform.position = spawnPosition;

    Item itemComponent = newItem.GetComponent<Item>();
    itemComponent.OnCollected += HandleItemCollected;
  }

  private Vector2 GetRandomScreenPos()
  {
    return new Vector2(Random.Range(minWorldPos.x, maxWorldPos.x), Random.Range(minWorldPos.y, maxWorldPos.y));
  }

  private void HandleItemCollected(Item collectedItem)
  {
    collectedItem.OnCollected -= HandleItemCollected; // Detach the event handler
    objectPool.ReturnObject(collectedItem.gameObject); // Return the collected item to the object pool

    SpawnItemWithDelay();
  }
}
