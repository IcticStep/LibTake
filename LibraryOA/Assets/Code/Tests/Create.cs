using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Tests
{
    public static class Create
    {
        public static Progress LogicProgress() =>
            new GameObject().AddComponent<Progress>();
    }
}