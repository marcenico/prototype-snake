using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
  None,
  Left,
  Right
}

public class GameOverBehaviour : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI textYes;
  [SerializeField] private TextMeshProUGUI textNo;
  [SerializeField] private string sceneInGame = "InGame";

  private Direction currentDirection = Direction.Left;

  private void Awake()
  {
    MoveOption();
  }

  // Update is called once per frame
  void Update()
  {
    if (currentDirection != GetLeftOrRight())
    {
      currentDirection = GetLeftOrRight();
      MoveOption();
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      ExcecuteSelectedOption();
    }
  }

  private Direction GetLeftOrRight()
  {
    bool isLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
    if (isLeft) return Direction.Left;

    bool isRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
    if (isRight) return Direction.Right;

    return currentDirection;
  }

  private void MoveOption()
  {
    switch (currentDirection)
    {
      case Direction.Left:
        textYes.color = Color.red;
        textNo.color = Color.white;
        break;
      case Direction.Right:
        textNo.color = Color.red;
        textYes.color = Color.white;
        break;
    }
  }

  private void ExcecuteSelectedOption()
  {
    switch (currentDirection)
    {
      case Direction.Left:
        OnYes();
        break;

      case Direction.Right:
        OnNo();
        break;
    }
  }

  public void OnYes()
  {
    SceneManager.LoadScene(sceneInGame);
  }

  public void OnNo()
  {
    Application.Quit();
  }
}