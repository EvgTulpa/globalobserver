# Example:
Class A and B starts asynchronously.

```c#
public class A : MonoBehaviour
{
  [SerializeField] private GameObject CoinsHolder;

  private void Start()
  {
      GlobalObserver.GetInstance().UpdateData("playerCoins", 123);
      GlobalObserver.GetInstance().UpdateData("coinsContainer", CoinsHolder);
  }
  
  private void OnDestroy()
  {
      GlobalObserver.GetInstance().TryRemoveDeferredData("coinsContainer", CoinsHolder);
  }
}
```

```c#
public class B : MonoBehaviour, IGlobalDataObject
{
  private int _playerCoins;

  private void Start()
  {
      GlobalObserver.GetInstance().Add("playerCoins", this);
  }
  
  private void OnDestroy()
  {
      GlobalObserver.GetInstance().Remove("playerCoins", this);
  }
  
  // IGlobalDataObject interfaces methods
  public void UpdateData(object data)
  {
      _playerCoins = (int)data;
  }
}
```
