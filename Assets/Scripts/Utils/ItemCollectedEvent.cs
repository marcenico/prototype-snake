using UnityEngine;

public class ItemCollectedEvent
{
  public Color CollectedColor { get; }

  public ItemCollectedEvent(Color color)
  {
    CollectedColor = color;
  }
}
