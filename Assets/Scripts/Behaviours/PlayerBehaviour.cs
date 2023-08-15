using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private Transform visual;
  [SerializeField] private GameObject segmentPrefab; // Prefab for the player segments
  private Rigidbody2D rb;
  private Vector2 moveDirection;
  private Vector2 lastMoveDirection = Vector2.zero;
  private int playerSize = 1;
  private Transform[] segments; // Array to hold the player segments
  private float segmentSpacing = 0.1f; // Spacing between segments

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f;
    InitializeSegments();
  }

  private void Update()
  {
    GetInputDirection();
  }

  private void FixedUpdate()
  {
    Move();
    MoveSegments();
  }

  private void InitializeSegments()
  {
    segments = new Transform[playerSize];
    segments[0] = visual;

    for (int i = 0; i < playerSize; i++)
    {
      GameObject newSegment = Instantiate(segmentPrefab, transform.position - new Vector3(0f, i * segmentSpacing, 0f), Quaternion.identity);
      segments[i] = newSegment.transform;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Item"))
    {
      IncreasePlayerSize();
      Destroy(other.gameObject);
    }
  }

  private void IncreasePlayerSize()
  {
    playerSize++;
    AddSegment();
  }

  private void GetInputDirection()
  {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    moveDirection.x = Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical) ? moveHorizontal : 0f;
    moveDirection.y = Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal) ? moveVertical : 0f;

    if (moveDirection == Vector2.zero) return;

    if (Vector2.Dot(moveDirection.normalized, lastMoveDirection.normalized) >= 0f)
    {
      lastMoveDirection = moveDirection.normalized;
    }
  }

  private void Move()
  {
    rb.velocity = lastMoveDirection * moveSpeed * Time.fixedDeltaTime * 100;

    if (lastMoveDirection != Vector2.zero)
    {
      float angle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg;
      visual.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
  }

  private void MoveSegments()
  {
    Vector3 prevPos = transform.position;

    for (int i = 0; i < playerSize; i++)
    {
      Vector3 targetPos = prevPos - new Vector3(0f, (i + 1) * segmentSpacing, 0f);
      segments[i].position = Vector3.Lerp(segments[i].position, targetPos, Time.fixedDeltaTime * moveSpeed);
      prevPos = segments[i].position;
    }
  }


  private void AddSegment()
  {
    Transform[] newSegments = new Transform[playerSize];
    for (int i = 0; i < playerSize - 1; i++)
    {
      newSegments[i] = segments[i];
    }

    GameObject newSegment = Instantiate(segmentPrefab, segments[playerSize - 2].position, Quaternion.identity);
    newSegments[playerSize - 1] = newSegment.transform;

    segments = newSegments;
  }

}
