using System;
using UnityEngine;

namespace Code.Runtime.Logic
{
    internal sealed class ActivateOnAwake : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target;

        private void Awake() =>
            _target.SetActive(true);
    }
}