using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  private Rigidbody2D rb;
  private Vector2 moveDirection;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f; // Freeze the gravity so that the player doesn't fall
  }

  private void FixedUpdate()
  {
    GetInputDirection();
    Move();
  }

  private void GetInputDirection()
  {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");

    // Use ternary operators to set the move direction
    moveDirection.x = Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical) ? moveHorizontal : 0f;
    moveDirection.y = Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal) ? moveVertical : 0f;
  }

  private void Move()
  {
    transform.position = BoundaryBehaviour.Instance.GetClampPosition(transform.position);
    rb.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime * 100;
  }
}
