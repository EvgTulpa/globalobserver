using System.Collections.Generic;

namespace CommonStructures
{
    public class GlobalObserver
    {
        private static GlobalObserver _instance;

        private readonly Dictionary<string, GlobalDataObjectsHolder> Holder;
        private readonly Dictionary<string, object> DeferredData;

        public static GlobalObserver Instance => _instance ?? (_instance = new GlobalObserver());

        private GlobalObserver()
        {
            Holder = new Dictionary<string, GlobalDataObjectsHolder>();
            DeferredData = new Dictionary<string, object>();
        }

        public void Add(string id, IGlobalDataObject dataObject)
        {
            if (dataObject == null || string.IsNullOrEmpty(id))
                return;
            
            if (!Holder.ContainsKey(id))
                Holder[id] = new GlobalDataObjectsHolder();
            
            Holder[id].Add(dataObject);
            
            if (DeferredData.ContainsKey(id))
            {
                Holder[id].UpdateData(DeferredData[id]);
                RemoveDeferredData(id);
            }
        }

        public bool Remove(string id, IGlobalDataObject dataObject)
        {
            if (dataObject == null || string.IsNullOrEmpty(id))
                return false;

            if (!Holder.ContainsKey(id))
                return false;
            
            return Holder[id].Remove(dataObject);
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

            if (!Holder.ContainsKey(id))
            {
                DeferredData[id] = data;
                return false;
            }

            Holder[id].UpdateData(data);
            return true;
        }

        private void RemoveDeferredData(string id)
        {
            DeferredData[id] = null;
            DeferredData.Remove(id);
        }
    }
}
