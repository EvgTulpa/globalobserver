using System.Collections.Generic;

namespace CommonStructures
{
    public sealed class GlobalDataObjectsHolder
    {
        private readonly List<IGlobalDataObject> _globalDataObjects;

        public GlobalDataObjectsHolder()
        {
            _globalDataObjects = new List<IGlobalDataObject>();
        }

        public void Add(IGlobalDataObject dataObject)
        {
            if(dataObject == null)
                return;
            
            if(_globalDataObjects.Contains(dataObject))
                return;

            dataObject.OnDisposed += ConnectorDisposed;
            _globalDataObjects.Add(dataObject);
        }

        private void ConnectorDisposed(IGlobalDataObject dataObject)
        {
            dataObject.OnDisposed -= ConnectorDisposed;
            _globalDataObjects.Remove(dataObject);
        }

        public void UpdateData(object data)
        {
            _globalDataObjects.ForEach(e=>e.UpdateData(data));
        }
    }
}
