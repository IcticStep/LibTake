using Code.Runtime.Logic;
using UnityEngine;

namespace Code.EditorTests
{
    public static class Create
    {
        public static Progress LogicProgress() =>
            new GameObject().AddComponent<Progress>();
    }
}