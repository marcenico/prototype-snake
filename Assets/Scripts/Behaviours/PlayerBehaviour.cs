using UnityEngine;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private float rotationSpeed = 15f;
  [SerializeField] private GameObject segmentPrefab;

  private Rigidbody2D rb;
  private Vector2 moveDirection = Vector2.up;
  public List<Transform> segments = new();
  private readonly float segmentSpacing = 0.2f;

  private readonly Dictionary<KeyCode, Vector2> directionMappings = new()
  {
    { KeyCode.W, Vector2.up },
    { KeyCode.S, Vector2.down },
    { KeyCode.A, Vector2.left },
    { KeyCode.D, Vector2.right }
  };

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f;
  }

  private void Start()
  {
    segments.Add(transform);
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
    if (other.CompareTag("Item")) IncreasePlayerSize();
  }

  private void IncreasePlayerSize()
  {
    Transform newSegment = Instantiate(segmentPrefab, transform.position, Quaternion.identity).transform;
    segments.Add(newSegment);
  }

  private void GetInputDirection()
  {
    foreach (var kvp in directionMappings)
    {
      if (Input.GetKeyDown(kvp.Key))
      {
        moveDirection = kvp.Value;
        break;
      }
    }
  }

  private void Move()
  {
    for (int i = segments.Count - 1; i > 0; i--)
    {
      segments[i].position = segments[i - 1].position;
    }

    Vector3 newPosition = transform.position + moveSpeed * Time.fixedDeltaTime * (Vector3)moveDirection;

    rb.MovePosition(newPosition);
    RotateThroughDirection();
  }

  private void RotateThroughDirection()
  {
    if (moveDirection == Vector2.zero) return;

    float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
    float currentAngle = transform.eulerAngles.z;
    float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.fixedDeltaTime * rotationSpeed);

    transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
  }

}
