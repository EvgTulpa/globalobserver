# Example:
Class A and B from different sytems starts asynchronously. You do not know wich object will start first, but you need to pass some data from A to B.
In this example the data by key "coinsContainer" will not be setted, but the "playerCoins" will be received.
If object A (data sender) will be created first, the object B (data receiver) still will receive the data after it will be added to GlobalObserver, because of DeferredData.<br>
<i>GlobalObserver.Instance.RemoveData</i> methods is recommended, because object A doesn`t know if object B will be created, so A need to try cleanup his data before it destruction.

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
      // need to cleanup after yourself
      GlobalObserver.Instance.RemoveData("playerCoins");
      GlobalObserver.Instance.RemoveData("coinsContainer");
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
