using UnityEngine;

public class EventManager : MonoBehaviour
{
  public static EventManager Instance { get; private set; }

  public delegate void ItemCollectedEventHandler(ItemCollectedEvent e);
  public event ItemCollectedEventHandler OnItemCollected;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void TriggerItemCollectedEvent(ItemCollectedEvent e)
  {
    OnItemCollected?.Invoke(e);
  }
}
