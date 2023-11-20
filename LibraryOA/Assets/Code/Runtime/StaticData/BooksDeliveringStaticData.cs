using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Books delivering", menuName = "Static data/Books delivering")]
    public class BooksDeliveringStaticData : ScriptableObject
    {
        [field: SerializeField]
        public int BooksPerDeliveringAmount { get; private set; } = 5;
    }
}