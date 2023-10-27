using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
  [SerializeField] private float rotationSpeed = 15f;
  [SerializeField] private GameObject segmentPrefab;
  [SerializeField] private int initialSize = 5;
  [SerializeField] private float gridUnit = 0.3f; // Size of each grid unit
  [SerializeField] private float initialSpeed = 5f;
  [SerializeField] private float speedIncreaseFactor = 0.05f;


  private readonly float directionChangeCooldown = 0.05f;
  private Rigidbody2D rigidBody2D;
  private Vector2 moveDirection = Vector2.up;
  private readonly List<Transform> segments = new();
  private float lastDirectionChangeTime = 0f;
  private float moveSpeed;

  private readonly Dictionary<KeyCode, Vector2> directionMappings = new()
    {
        { KeyCode.W, Vector2.up },
        { KeyCode.S, Vector2.down},
        { KeyCode.A, Vector2.left },
        { KeyCode.D, Vector2.right }
    };

  private void Awake()
  {
    rigidBody2D = GetComponent<Rigidbody2D>();
    rigidBody2D.gravityScale = 0f;
    moveSpeed = initialSpeed;
  }

  private void Start()
  {
    InitializeSnake();
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
    switch (other.tag)
    {
      case "Item":
        AddSegment();
        break;
      case "Segment":
        GameManager.Instance.GameOver();
        break;
    }
  }

  void InitializeSnake()
  {
    for (int i = 0; i < initialSize; i++)
    {
      AddSegment();
    }
  }

  void AddSegment()
  {
    Vector2 instantiatePosition = segments.Count > 0 ? segments[segments.Count - 1].position : transform.position;
    GameObject newSegment = Instantiate(segmentPrefab, instantiatePosition, Quaternion.identity);
    segments.Add(newSegment.transform);
    moveSpeed += speedIncreaseFactor; // Increase the speed
  }

  private void GetInputDirection()
  {
    foreach (var kvp in directionMappings)
    {
      if (Input.GetKeyDown(kvp.Key) && Time.time - lastDirectionChangeTime >= directionChangeCooldown)
      {
        // Prevent setting moveDirection to the opposite direction
        if (moveDirection != -kvp.Value)
        {
          moveDirection = kvp.Value;
          lastDirectionChangeTime = Time.time;
        }
        break;
      }
    }
  }

  void Move()
  {
    float moveDistance = gridUnit * moveSpeed * Time.fixedDeltaTime; // Snake moves one grid unit per frame

    Vector2 previousPosition = transform.position;
    transform.Translate(moveDirection * moveDistance, Space.World);

    for (int i = segments.Count - 1; i > 0; i--)
    {
      Transform currentSegment = segments[i];
      Transform previousSegment = segments[i - 1];
      currentSegment.position = previousSegment.position;
    }

    if (segments.Count > 0)
    {
      segments[0].position = previousPosition;
    }

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

  public void ResetPlayer()
  {
    // Destroy all segments instancianted
    foreach (Transform segment in segments)
    {
      Destroy(segment.gameObject);
    }

    segments.Clear();
    transform.position = Vector2.zero;
    moveSpeed = initialSpeed;

    // Initialize the snake again
    InitializeSnake();
  }
}
