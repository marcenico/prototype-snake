using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  private Rigidbody2D rb;
  private Vector2 moveDirection;
  private Vector2 lastMoveDirection = Vector2.zero;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f; // Freeze the gravity so that the player doesn't fall
  }

  private void Update()
  {
    GetInputDirection();
  }

  private void FixedUpdate()
  {
    Move();
  }

  private void GetInputDirection()
  {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    // Use ternary operators to set the move direction
    moveDirection.x = Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical) ? moveHorizontal : 0f;
    moveDirection.y = Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal) ? moveVertical : 0f;

    // Return if the last input direction it zero
    if (moveDirection == Vector2.zero) return;

    // Check if the new input is not opposite to the lastMoveDirection
    if (Vector2.Dot(moveDirection.normalized, lastMoveDirection.normalized) >= 0f)
    {
      lastMoveDirection = moveDirection.normalized;
    }

  }

  private void Move()
  {
    transform.position = BoundaryBehaviour.Instance.GetClampPosition(transform.position);
    rb.velocity = lastMoveDirection * moveSpeed * Time.fixedDeltaTime * 100;
  }
}
