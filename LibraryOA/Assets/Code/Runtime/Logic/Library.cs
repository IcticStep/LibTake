using UnityEngine;

namespace Code.Runtime.Logic
{
    internal sealed class Library : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer[] _secondFloorMeshes;

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