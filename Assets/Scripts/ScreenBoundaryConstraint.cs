using UnityEngine;

public class ScreenBoundaryConstraint : MonoBehaviour
{
  private Camera mainCamera;
  private float objectWidth;
  private float objectHeight;

  private void Awake()
  {
    mainCamera = Camera.main;
    CalculateObjectSize();
  }

  private void CalculateObjectSize()
  {
    // Calculate the size of the object's sprite or collider
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    Collider2D collider = GetComponent<Collider2D>();

    if (spriteRenderer != null)
    {
      objectWidth = spriteRenderer.bounds.extents.x;
      objectHeight = spriteRenderer.bounds.extents.y;
    }
    else if (collider != null)
    {
      objectWidth = collider.bounds.extents.x;
      objectHeight = collider.bounds.extents.y;
    }
    else
    {
      // If no sprite renderer or collider found, use default values
      objectWidth = 0.5f;
      objectHeight = 0.5f;
    }
  }

  private void FixedUpdate()
  {
    ConstrainObjectPosition();
  }

  private void ConstrainObjectPosition()
  {
    // Get the screen boundaries in world coordinates
    float screenAspect = (float)Screen.width / Screen.height;
    float screenOrthographicSize = mainCamera.orthographicSize;
    float screenWidth = screenAspect * screenOrthographicSize;
    float screenHeight = screenOrthographicSize;

    // Calculate the clamped position considering the object's size
    float clampedX = Mathf.Clamp(transform.position.x, -screenWidth + objectWidth, screenWidth - objectWidth);
    float clampedY = Mathf.Clamp(transform.position.y, -screenHeight + objectHeight, screenHeight - objectHeight);

    // Move the object to the clamped position
    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
  }
}
