# Example:
Class A and B starts asynchronously.

```c#
public class A : MonoBehaviour
{
  private void Start()
  {
      GlobalObserver.GetInstance().UpdateData("playerCoins", 123);
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
      OnDisposed?.Invoke(this);
  }
  
  // IGlobalDataObject interfaces methods
  public void UpdateData(object data)
  {
      _playerCoins = (int)data;
  }

  public event Action<IGlobalDataObject> OnDisposed;
}
```
