using UnityEngine;

namespace Code.Runtime.StaticData.Balance
{
    [CreateAssetMenu(fileName = "Customers delivering", menuName = "Static data/Customers delivering")]
    public class CustomersDeliveringStaticData : ScriptableObject
    {
        [field: SerializeField]
        public int BooksPerDeliveringAmount { get; private set; } = 5;
    }
}