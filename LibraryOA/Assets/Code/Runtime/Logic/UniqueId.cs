using UnityEngine;

namespace Code.Runtime.Logic
{
    public sealed class UniqueId : MonoBehaviour, IUniqueIdInitializer
    {
        [SerializeField] private string _id;
        string IUniqueIdInitializer.Id { get => _id; set => _id = value; }
        public string Id => _id;
    }
}