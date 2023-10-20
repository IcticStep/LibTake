using UnityEngine;

namespace Code.Runtime.Logic
{
    public sealed class UniqueId : MonoBehaviour
    {
        [SerializeField] private string _id;
        public string Id => _id;

        public void InitId(string id) =>
            _id = id;
    }
}