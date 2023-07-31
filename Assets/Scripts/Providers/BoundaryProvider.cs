public class BoundaryProvider {
  public delegate Boundary GetBoundaries();
  public static event GetBoundaries OnGetBoundaries;

  public static void TriggerOnGetBoundaries() {
    OnGetBoundaries?.Invoke();
  }
}