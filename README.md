Example:
public class A : MonoBehaviour{
  private void Start()
  {
    GlobalObserver.GetInstance().UpdateData("playerCoins", 123);
  }
}
