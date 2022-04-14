using System.Collections.Generic;

namespace CommonStructures
{
    public sealed class GlobalDataObjectsHolder
    {
        private readonly List<IGlobalDataObject> _gameDataObjects;

        public GlobalDataObjectsHolder()
        {
            _gameDataObjects = new List<IGlobalDataObject>();
        }

        public void Add(IGlobalDataObject dataObject)
        {
            if(dataObject == null)
                return;
            
            if(_gameDataObjects.Contains(dataObject))
                return;

            dataObject.OnDisposed += ConnectorDisposed;
            _gameDataObjects.Add(dataObject);
        }

        private void ConnectorDisposed(IGlobalDataObject dataObject)
        {
            dataObject.OnDisposed -= ConnectorDisposed;
            _gameDataObjects.Remove(dataObject);
        }

        public void UpdateData(object data)
        {
            _gameDataObjects.ForEach(e=>e.UpdateData(data));
        }
    }
}