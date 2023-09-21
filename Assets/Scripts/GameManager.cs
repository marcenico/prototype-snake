using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; } // Singleton instance

  [SerializeField] private PlayerBehaviour player; // Reference to the player GameObject
  [SerializeField] private ItemSpawner spawner; // Reference to the spawner GameObject


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
    player?.gameObject.SetActive(false);
  }

  public void StartGame()
  {
    player?.gameObject.SetActive(true);
    spawner.SpawnItem();
  }

  public void GameOver()
  {
    // Reset player settings
    player.ResetPlayer();
  }
}