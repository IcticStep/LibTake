using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class ProgressesStates
    {
        [SerializeField]
        private List<IdentifiedData<float>> _progressValues = new();

        public float GetDataForId(string id) =>
            (_progressValues.FirstOrDefault(x => x.Id == id)?.Data)
            ?? default(float);

        public void SetDataForId(string id, float value)
        {
            IdentifiedData<float> savedData = _progressValues.FirstOrDefault(x => x.Id == id);

            if(savedData is not null)
                _progressValues.Remove(savedData);

            _progressValues.Add(new IdentifiedData<float>(id, value));
        }
    }
}