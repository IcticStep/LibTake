using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Logic
{
    internal sealed class Library : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer[] _secondFloorMeshes;
        [SerializeField]
        private bool _hideSecondFloorOnStart = true;

        private void Start()
        {
            if(_hideSecondFloorOnStart)
                HideSecondFloor();
        }

        public void ShowSecondFloor()
        {
            for(int i = 0; i < _secondFloorMeshes.Length; i++)
                _secondFloorMeshes[i].enabled = true;
        }
        
        public void HideSecondFloor()
        {
            for(int i = 0; i < _secondFloorMeshes.Length; i++)
                _secondFloorMeshes[i].enabled = false;
        }
    }
}