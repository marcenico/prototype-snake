using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; } // Singleton instance

  [SerializeField] private GameObject player; // Reference to the player GameObject


  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }
  }

  private void Start()
  {
    BeforeStartGame();
  }

  private void BeforeStartGame()
  {
    player?.SetActive(false);
  }

  public void StartGame()
  {
    player?.SetActive(true);
  }
}