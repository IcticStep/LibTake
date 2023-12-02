using UnityEngine;

namespace Code.Runtime.StaticData.Balance
{
    [CreateAssetMenu(fileName = "Customer", menuName = "Static data/Customer")]
    public class StaticCustomer : ScriptableObject
    {
        [field: SerializeField]
        public float TimeToReceiveBook { get; private set; }
    }
}