using UnityEngine;

public class CameraWalls : MonoBehaviour
{
  private Camera mainCamera;
  private float cameraHeight;
  private float cameraWidth;
  private readonly string wallTag = "Obstacle";

  private void Start()
  {
    mainCamera = Camera.main;
    cameraHeight = mainCamera.orthographicSize;
    cameraWidth = cameraHeight * mainCamera.aspect;

    CreateWalls();
  }

  private void CreateWalls()
  {
    string[] wallNames = { "TopWall", "BottomWall", "RightWall", "LeftWall" };
    Vector3[] wallPositions = {
            new Vector3(0f, cameraHeight + 0.5f, 0f),
            new Vector3(0f, -cameraHeight - 0.5f, 0f),
            new Vector3(cameraWidth + 0.5f, 0f, 0f),
            new Vector3(-cameraWidth - 0.5f, 0f, 0f)
        };
    Vector2[] colliderSizes = {
            new Vector2(cameraWidth * 2f, 1f),
            new Vector2(cameraWidth * 2f, 1f),
            new Vector2(1f, cameraHeight * 2f),
            new Vector2(1f, cameraHeight * 2f)
        };

    for (int i = 0; i < 4; i++)
    {
      GameObject wall = new GameObject(wallNames[i]);
      wall.tag = wallTag;
      wall.transform.parent = transform; // Set as child of the GameObject with this script
      BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
      collider.size = colliderSizes[i];
      wall.transform.position = wallPositions[i];
      collider.isTrigger = true; // Set as a physical wall
    }
  }
}
