using UnityEngine;

public class WindowResizeListener : MonoBehaviour {
  private int lastScreenWidth = 0;
  private int lastScreenHeight = 0;

  private void Awake() {
    GetBoundaries();
  }

  private void Update() {
    if (lastScreenWidth != Screen.width || lastScreenHeight != Screen.height) {
      lastScreenWidth = Screen.width;
      lastScreenHeight = Screen.height;
      GetBoundaries();
    }
  }

  private void GetBoundaries() {
    BoundaryProvider.TriggerOnGetBoundaries();
  }
}