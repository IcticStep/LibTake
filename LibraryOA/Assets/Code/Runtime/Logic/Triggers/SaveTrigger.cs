using Code.Runtime.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Triggers
{
    internal sealed class SaveTrigger : MonoBehaviour
    {
        [SerializeField]
        private Trigger _trigger;
        
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;
        
        private void Awake() =>
            _trigger.Entered += Save;
        
        private void OnDestroy() =>
            _trigger.Entered -= Save;
        
        private void Save()
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved!");
            gameObject.SetActive(false);
        }
    }
}