using UnityEngine;

[System.Serializable]
public class Boundary
{
  public float left = 0;
  public float right = 0;
  public float top = 0;
  public float bottom = 0;
}

public class BoundaryBehaviour : MonoBehaviour
{
  #region Singleton Pattern
  private static BoundaryBehaviour instance;

  public static BoundaryBehaviour Instance
  {
    get
    {
      if (instance is null)
      {
        GameObject go = new GameObject("BoundaryBehaviour");
        go.AddComponent<BoundaryBehaviour>();
        go.AddComponent<WindowResizeListener>();
      }
      return instance;
    }
  }
  #endregion

  [SerializeField] private float margin = 1f;
  public Boundary boundaries = new Boundary();

  private void Awake()
  {
    instance = this;
    BoundaryProvider.OnGetBoundaries += OnGetBoundaries;
  }


  public Boundary OnGetBoundaries()
  {
    Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
    Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    boundaries.left = lowerLeft.x + margin;
    boundaries.right = upperRight.x - margin;
    boundaries.top = upperRight.y - margin;
    boundaries.bottom = lowerLeft.y + margin;
    return boundaries;
  }

  public Vector3 GetClampPosition(Vector3 position)
  {
    float x = Mathf.Clamp(position.x, boundaries.left, boundaries.right);
    float y = Mathf.Clamp(position.y, boundaries.bottom, boundaries.top);
    return new Vector3(x, y);
  }

  private void OnDestroy()
  {
    BoundaryProvider.OnGetBoundaries -= OnGetBoundaries;
  }
}