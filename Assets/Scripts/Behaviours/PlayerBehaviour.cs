using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private Transform visual;
  [SerializeField] private GameObject segmentPrefab; // Prefab for the player segments

  private Rigidbody2D rb;
  private Vector2 moveDirection;
  private Vector2 lastMoveDirection = Vector2.zero;
  private int playerSize = 0;
  private Transform[] segments; // Array to hold the player segments
  private float segmentSpacing = -1f; // Spacing between segments

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f;
  }

  private void Update()
  {
    GetInputDirection();
  }

  private void FixedUpdate()
  {
    Move();
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
    Transform newSegment = Instantiate(segmentPrefab, transform.localPosition - new Vector3(playerSize * segmentSpacing, 0f, 0f), Quaternion.identity).transform;
    newSegment.parent = transform;
    segments[playerSize] = newSegment.transform;
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

}
