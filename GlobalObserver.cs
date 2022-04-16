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

        public void Add(string id, IGlobalDataObject dataObject)
        {
            if (!IsParamsValid(id, dataObject))
                return;
            
            if (!IsDataHolderExist(id))
                DataHolders[id] = new GlobalDataObjectsHolder();
            
            DataHolders[id].Add(dataObject);
            UpdateHolderData(id, IsDeferredDataExist(id) ? DeferredData[id] : null);
        }
        
        public void UpdateData(string id, object data)
        {
            if (!IsParamsValid(id, data))
                return;

            if (!IsDataHolderExist(id))
            {
                DeferredData[id] = data;
                return;
            }
            
            UpdateHolderData(id, data);
        }
        
        private void UpdateHolderData(string id, object data)
        {
            if (data != null)
                DataHolders[id].UpdateData(data);
        }

        public void Remove(string id, IGlobalDataObject dataObject)
        {
            if (!IsParamsValid(id, dataObject))
                return;

            if (!IsDataHolderExist(id))
                return;

            bool isEmpty = DataHolders[id].RemoveAndCheckIsEmpty(dataObject);
            if (isEmpty)
                DataHolders.Remove(id);
        }

        public void RemoveData(string id)
        {
            if(string.IsNullOrEmpty(id))
                return;
            
            if(IsDeferredDataExist(id))
                DeferredData.Remove(id);
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
    }
}
