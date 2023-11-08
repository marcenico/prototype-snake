using UnityEngine;

[CreateAssetMenu(fileName = "New Color Set", menuName = "ScriptableObjects/Color Set", order = 1)]
public class ColorSet : ScriptableObject
{
  public Color[] colors;
}
