# Example:
Class A and B from different sytems starts asynchronously. You don`t know wich object will start first, but you need to pass some data from A to B.

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
      // use this method if type of passed object is reference type, to clean up after yourself
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
