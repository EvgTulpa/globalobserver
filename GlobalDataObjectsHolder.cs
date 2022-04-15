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

            _globalDataObjects.Add(dataObject);
        }

        public bool Remove(IGlobalDataObject dataObject)
        {
            return dataObject != null && _globalDataObjects.Remove(dataObject);
        }
        
        public void UpdateData(object data)
        {
            _globalDataObjects.ForEach(e=>e.UpdateData(data));
        }
    }
}
