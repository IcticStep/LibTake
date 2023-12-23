using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Tests.Logic
{
    public static class Create
    {
        public static Progress Progress() =>
            new GameObject().AddComponent<Progress>();
    }
}