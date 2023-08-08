using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
  public UnityAction<Item> OnCollected;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Collect();
    }
  }

  private void Collect()
  {
    OnCollected?.Invoke(this);
    Destroy(gameObject);
  }
}
