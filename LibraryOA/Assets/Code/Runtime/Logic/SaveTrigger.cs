using Code.Runtime.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic
{
    internal sealed class SaveTrigger : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider _collider;
        
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;
        
        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved!");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if(!_collider) return;
      
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}