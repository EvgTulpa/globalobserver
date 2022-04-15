# Example:
Class A and B from different sytems starts asynchronously. You don`t know wich object will start first, but you need to pass some data from A to B.
In this example the data by key "coinsContainer" will not be setted.
If object A will be created first, the object B will receive the data after it will be added to GlobalObserver, because of DeferredData.

```c#
public class A : MonoBehaviour
{
  [SerializeField] private GameObject CoinsHolder;

  private void Start()
  {
      GlobalObserver.Instance.UpdateData("playerCoins", 123);
      GlobalObserver.Instance.UpdateData("coinsContainer", CoinsHolder);
  }
  
  private void OnDestroy()
  {
      GlobalObserver.Instance.DisposeData("coinsContainer", CoinsHolder);
      GlobalObserver.Instance.DisposeData("playerCoins", 123);
  }
}
```

```c#
public class B : MonoBehaviour, IGlobalDataObject
{
  private int _playerCoins;

  private void Start()
  {
      GlobalObserver.Instance.Add("playerCoins", this);
  }
  
  private void OnDestroy()
  {
      GlobalObserver.Instance.Remove("playerCoins", this);
  }
  
  // IGlobalDataObject interfaces methods
  public void UpdateData(object data)
  {
      _playerCoins = (int)data;
  }
}
```
