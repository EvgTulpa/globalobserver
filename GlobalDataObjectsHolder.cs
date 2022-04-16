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
            if(GlobalDataObjects.Contains(dataObject))
                return;

            GlobalDataObjects.Add(dataObject);
        }

        public bool RemoveAndCheckIsEmpty(IGlobalDataObject dataObject)
        {
            GlobalDataObjects.Remove(dataObject);
            return GlobalDataObjects.Count == 0;
        }
        
        public void UpdateData(object data)
        {
            GlobalDataObjects.ForEach(e=>e.UpdateData(data));
        }
    }
}
