using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    internal class IdentifiedData<T>
    {
        public string Id;
        public T Data;

        public IdentifiedData(string id, T data)
        {
            Id = id;
            Data = data;
        }
    }
}