using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraColorChanger : MonoBehaviour
{
  [SerializeField] private float transitionTime = 1f;
  private Camera mainCamera = null;

  private void Awake()
  {
    mainCamera = GetComponent<Camera>();
  }

  private void Start()
  {
    ColorManager.Instance.OnChangeColor += OnChangeColor;
  }

  private void OnChangeColor(Color newColor)
  {
    StartCoroutine(ChangeCameraColorSmoothly(newColor));
  }

  private IEnumerator ChangeCameraColorSmoothly(Color newColor)
  {
    Color startColor = mainCamera.backgroundColor;
    float elapsedTime = 0f;

    while (elapsedTime < transitionTime)
    {
      mainCamera.backgroundColor = Color.Lerp(startColor, newColor, elapsedTime / transitionTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }

    mainCamera.backgroundColor = newColor;
  }
}
