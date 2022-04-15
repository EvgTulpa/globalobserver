using System.Collections.Generic;

namespace CommonStructures
{
    public class GlobalObserver
    {
        private static GlobalObserver _instance;

        private readonly Dictionary<string, GlobalDataObjectsHolder> _holder;
        private readonly Dictionary<string, object> _deferredData;

        public static GlobalObserver GetInstance()
        {
            return _instance ?? (_instance = new GlobalObserver());
        }

        private GlobalObserver()
        {
            _holder = new Dictionary<string, GlobalDataObjectsHolder>();
            _deferredData = new Dictionary<string, object>();
        }

        public void Add(string id, IGlobalDataObject dataObject)
        {
            if (dataObject == null || string.IsNullOrEmpty(id))
                return;
            
            if (!_holder.ContainsKey(id))
                _holder[id] = new GlobalDataObjectsHolder();
            
            _holder[id].Add(dataObject);
            
            if (_deferredData.ContainsKey(id))
            {
                _holder[id].UpdateData(_deferredData[id]);
                RemoveDeferredData(id);
            }
        }

        public bool Remove(string id, IGlobalDataObject dataObject)
        {
            if (dataObject == null || string.IsNullOrEmpty(id))
                return false;

            if (!_holder.ContainsKey(id))
                return false;
            
            return _holder[id].Remove(dataObject);
        }

        public bool TryRemoveDeferredData(string id, object data)
        {
            if (data == null || string.IsNullOrEmpty(id))
                return false;
            
            if (!_deferredData.ContainsKey(id))
                return false;
            
            RemoveDeferredData(id);
            return true;
        }
        
        public bool UpdateData(string id, object data)
        {
            if (data == null || string.IsNullOrEmpty(id))
                return false;

            if (!_holder.ContainsKey(id))
            {
                _deferredData[id] = data;
                return false;
            }

            _holder[id].UpdateData(data);
            return true;
        }

        private void RemoveDeferredData(string id)
        {
            _deferredData[id] = null;
            _deferredData.Remove(id);
        }
    }
}
