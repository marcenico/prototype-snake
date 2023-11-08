using UnityEngine;

public class ColorManager : MonoBehaviour
{
  [SerializeField] private ColorSet colorSet;
  [SerializeField] private Color defaultColor = Color.HSVToRGB(253, 36, 97);

  public static ColorManager Instance { get; private set; }

  public delegate void ChangeColorEventHandler(Color newColor);
  public event ChangeColorEventHandler OnChangeColor;

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

  public void ChangeBackgroundColor()
  {
    Color newColor = GetRandomColor();
    OnChangeColor?.Invoke(newColor);
  }

  Color GetRandomColor()
  {
    if (colorSet != null && colorSet.colors.Length > 0)
    {
      int randomColorIndex = Random.Range(0, colorSet.colors.Length);
      return colorSet.colors[randomColorIndex];
    }

    return defaultColor; // Cambia esto a tu color por defecto si es necesario
  }
}
