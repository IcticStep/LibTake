using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Runtime.Infrastructure.AssetManagement
{
    [UsedImplicitly]
    public class AssetProvider : IAssetProvider
    {
        private readonly DiContainer _container;

        public AssetProvider(DiContainer container)
        {
            _container = container;
        }

        public GameObject Instantiate(GameObject prefab, Transform parent = null) =>
            Instantiate(prefab, Vector3.zero, parent);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent = null) =>
            Instantiate(prefab, at, Quaternion.identity, parent);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent = null)
        {
            GameObject result = _container.InstantiatePrefab(prefab, at, rotation, parent);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            return result;
        }
        
        public GameObject Instantiate(string path, Transform parent = null) =>
            Instantiate(path, Vector3.zero, parent);

        public GameObject Instantiate(string path, Vector3 at, Transform parent = null) =>
            Instantiate(path, at, Quaternion.identity, parent);

        public GameObject Instantiate(string path, Vector3 at, Quaternion rotation, Transform parent = null)
        {
            GameObject instance = Resources.Load<GameObject>(path);
            GameObject result = _container.InstantiatePrefab(instance, at, rotation, parent);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            return result;
        }
    }
}