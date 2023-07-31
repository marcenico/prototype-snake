using UnityEngine;

public class Boundaries : MonoBehaviour
{
  public GameObject boundaryPrefab;

  private void Start()
  {
    CreateBoundaries();
  }

  private void CreateBoundaries()
  {
    if (Camera.main == null)
    {
      Debug.LogError("Main camera not found. Make sure you have a camera in the scene tagged as MainCamera.");
      return;
    }

    float cameraHeight = Camera.main.orthographicSize * 2f;
    float cameraWidth = cameraHeight * Camera.main.aspect;

    // Create the top boundary
    GameObject topBoundary = Instantiate(boundaryPrefab, transform);
    topBoundary.name = "TopBoundary";
    topBoundary.transform.position = new Vector3(0f, cameraHeight / 2f, 0f);
    BoxCollider2D topCollider = topBoundary.AddComponent<BoxCollider2D>();
    topCollider.size = new Vector2(cameraWidth, 1f);
    topCollider.isTrigger = true;

    // Create the right boundary
    GameObject rightBoundary = Instantiate(boundaryPrefab, transform);
    rightBoundary.name = "RightBoundary";
    rightBoundary.transform.position = new Vector3(cameraWidth / 2f, 0f, 0f);
    BoxCollider2D rightCollider = rightBoundary.AddComponent<BoxCollider2D>();
    rightCollider.size = new Vector2(1f, cameraHeight);
    rightCollider.isTrigger = true;

    // Create the bottom boundary
    GameObject bottomBoundary = Instantiate(boundaryPrefab, transform);
    bottomBoundary.name = "BottomBoundary";
    bottomBoundary.transform.position = new Vector3(0f, -cameraHeight / 2f, 0f);
    BoxCollider2D bottomCollider = bottomBoundary.AddComponent<BoxCollider2D>();
    bottomCollider.size = new Vector2(cameraWidth, 1f);
    bottomCollider.isTrigger = true;

    // Create the left boundary
    GameObject leftBoundary = Instantiate(boundaryPrefab, transform);
    leftBoundary.name = "LeftBoundary";
    leftBoundary.transform.position = new Vector3(-cameraWidth / 2f, 0f, 0f);
    BoxCollider2D leftCollider = leftBoundary.AddComponent<BoxCollider2D>();
    leftCollider.size = new Vector2(1f, cameraHeight);
    leftCollider.isTrigger = true;
  }

  private void OnTrigger(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      ConstrainToScreenBounds(other.transform);
    }
  }

  private void ConstrainToScreenBounds(Transform objectTransform)
  {
    // Get the screen boundaries in world coordinates
    Vector3 screenLimits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

    // Clamp the object's position to stay within the screen boundaries
    float clampedX = Mathf.Clamp(objectTransform.position.x, -screenLimits.x + objectTransform.localScale.x / 2f, screenLimits.x - objectTransform.localScale.x / 2f);
    float clampedY = Mathf.Clamp(objectTransform.position.y, -screenLimits.y + objectTransform.localScale.y / 2f, screenLimits.y - objectTransform.localScale.y / 2f);

    objectTransform.position = new Vector3(clampedX, clampedY, objectTransform.position.z);
  }
}
