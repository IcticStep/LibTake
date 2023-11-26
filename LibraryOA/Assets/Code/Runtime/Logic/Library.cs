using System.Linq;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public sealed class Library : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _secondFloorObjects;
        [SerializeField]
        private bool _hideSecondFloorOnStart = true;
        
        private MeshRenderer[] _secondFloorMeshes;

        private void Awake()
        {
            _secondFloorMeshes = _secondFloorObjects
                .Select(x => x.GetComponentInChildren<MeshRenderer>())
                .ToArray();

            TurnOnSecondFloor();
            
            if(_hideSecondFloorOnStart)
                HideSecondFloor();
        }

        public void ShowSecondFloor()
        {
            foreach(MeshRenderer meshRenderer in _secondFloorMeshes)
                meshRenderer.enabled = true;
        }
        
        public void HideSecondFloor()
        {
            foreach(MeshRenderer meshRenderer in _secondFloorMeshes)
                meshRenderer.enabled = false;
        }

        public void TurnOnSecondFloor()
        {
            foreach(GameObject gameObject in _secondFloorObjects)
                gameObject.SetActive(true);
        }

        public void TurnOffSecondFloor()
        {
            foreach(GameObject gameObject in _secondFloorObjects)
                gameObject.SetActive(false);
        }
    }
}