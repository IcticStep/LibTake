using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Runtime.Infrastructure.Services
{
    public class SwitchToEntryScene : MonoBehaviour
    {
#if UNITY_EDITOR
        private const int EntrySceneIndex = 0;

        private void Awake()
        {
            if (ProjectContext.HasInstance) 
                return;
      
            foreach (GameObject root in gameObject.scene.GetRootGameObjects()) 
                root.SetActive(false);
      
            SceneManager.LoadScene(EntrySceneIndex);
        }
#endif
    }
}