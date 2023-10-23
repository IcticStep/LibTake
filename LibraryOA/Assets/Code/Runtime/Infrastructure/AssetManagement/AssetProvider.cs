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

        public GameObject Instantiate(string path, Vector3 at, Transform parent = null) =>
            Instantiate(path, at, Quaternion.identity, parent);

        public GameObject Instantiate(string path, Vector3 at, Quaternion rotation, Transform parent = null)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            GameObject result = _container.InstantiatePrefab(prefab, at, rotation, parent);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            return result;
        }
    }
}