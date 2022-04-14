using System;

namespace CommonStructures
{
    public interface IGlobalDataObject
    {
        void UpdateData(object data);
        event Action<IGlobalDataObject> OnDisposed;
    }
}