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
            if (!IsParamsValid(id, dataObject))
                return false;
            
            if (!IsDataHolderExist(id))
                DataHolders[id] = new GlobalDataObjectsHolder();
            
            DataHolders[id].Add(dataObject);
            
            if (IsDeferredDataExist(id))
            {
                DataHolders[id].UpdateData(DeferredData[id]);
                RemoveDeferredData(id);
            }
            
            return true;
        }

        public bool Remove(string id, IGlobalDataObject dataObject)
        {
            if (!IsParamsValid(id, dataObject))
                return false;

            if (!IsDataHolderExist(id))
                return false;

            bool isEmpty = DataHolders[id].RemoveAndCheckIsEmpty(dataObject);
            if (isEmpty)
                DataHolders.Remove(id);
            
            return true;
        }

        public bool UpdateData(string id, object data)
        {
            if (!IsParamsValid(id, data))
                return false;

            if (!IsDataHolderExist(id))
            {
                DeferredData[id] = data;
                return false;
            }

            DataHolders[id].UpdateData(data);
            return true;
        }
        
        public bool DisposeData(string id, object data)
        {
            if (!IsParamsValid(id, data))
                return false;
            
            if (!IsDeferredDataExist(id))
                return false;
            
            return RemoveDeferredData(id);
        }

        private bool IsParamsValid(string id, object data)
        {
            return !string.IsNullOrEmpty(id) && data != null;
        }

        private bool IsDataHolderExist(string id)
        {
            return DataHolders.ContainsKey(id);
        }
        
        private bool IsDeferredDataExist(string id)
        {
            return DeferredData.ContainsKey(id);
        }

        private bool RemoveDeferredData(string id)
        {
            return DeferredData.Remove(id);
        }
    }
}
