using System.Collections.Generic;

namespace CommonStructures
{
    public sealed class GlobalDataObjectsHolder
    {
        private readonly List<IGlobalDataObject> GlobalDataObjects;

        public GlobalDataObjectsHolder()
        {
            GlobalDataObjects = new List<IGlobalDataObject>();
        }

        public void Add(IGlobalDataObject dataObject)
        {
            if(dataObject == null)
                return;
            
            if(GlobalDataObjects.Contains(dataObject))
                return;

            GlobalDataObjects.Add(dataObject);
        }

        public bool Remove(IGlobalDataObject dataObject)
        {
            return dataObject != null && GlobalDataObjects.Remove(dataObject);
        }
        
        public void UpdateData(object data)
        {
            GlobalDataObjects.ForEach(e=>e.UpdateData(data));
        }
    }
}
