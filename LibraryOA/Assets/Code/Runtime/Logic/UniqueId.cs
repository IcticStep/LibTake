using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public sealed class UniqueId : MonoBehaviour
    {
        [ReadOnly][SerializeField] private string _id;
        public string Id => _id;

        public void InitId(string id) =>
            _id = id;
    }
}