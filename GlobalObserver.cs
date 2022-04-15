using System.Collections.Generic;

namespace CommonStructures
{
    public sealed class GlobalObserver
    {
        private static GlobalObserver _instance;

        private readonly Dictionary<string, GlobalDataObjectsHolder> DataHolders;
        private readonly Dictionary<string, object> DeferredData;

        public static GlobalObserver Instance => _instance ?? (_instance = new GlobalObserver());

        private GlobalObserver()
        {
            DataHolders = new Dictionary<string, GlobalDataObjectsHolder>();
            DeferredData = new Dictionary<string, object>();
        }

        public bool Add(string id, IGlobalDataObject dataObject)
        {
            if (dataObject == null || string.IsNullOrEmpty(id))
                return false;
            
            if (!DataHolders.ContainsKey(id))
                DataHolders[id] = new GlobalDataObjectsHolder();
            
            DataHolders[id].Add(dataObject);
            
            if (DeferredData.ContainsKey(id))
            {
                DataHolders[id].UpdateData(DeferredData[id]);
                RemoveDeferredData(id);
            }
            
            return true;
        }

        public bool Remove(string id, IGlobalDataObject dataObject)
        {
            if (dataObject == null || string.IsNullOrEmpty(id))
                return false;

            if (!DataHolders.ContainsKey(id))
                return false;
            
            return DataHolders[id].Remove(dataObject);
        }

        public bool DisposeData(string id, object data)
        {
            if (data == null || string.IsNullOrEmpty(id))
                return false;
            
            if (!DeferredData.ContainsKey(id))
                return false;
            
            RemoveDeferredData(id);
            return true;
        }
        
        public bool UpdateData(string id, object data)
        {
            if (data == null || string.IsNullOrEmpty(id))
                return false;

            if (!DataHolders.ContainsKey(id))
            {
                DeferredData[id] = data;
                return false;
            }

            DataHolders[id].UpdateData(data);
            return true;
        }

        private void RemoveDeferredData(string id)
        {
            DeferredData[id] = null;
            DeferredData.Remove(id);
        }
    }
}
