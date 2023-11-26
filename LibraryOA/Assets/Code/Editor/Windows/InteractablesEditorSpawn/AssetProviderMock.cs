using Code.Runtime.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor.Windows.InteractablesEditorSpawn
{
    internal sealed class AssetProviderMock : IAssetProvider
    {
        public GameObject Instantiate(GameObject prefab, Transform parent = null) =>
            Instantiate(prefab, Vector3.zero, parent);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent = null) =>
            Instantiate(prefab, at, Quaternion.identity, parent);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent = null)
        {
            GameObject result = Object.Instantiate(prefab, at, rotation, parent);
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
            GameObject result = Object.Instantiate(instance, at, rotation, parent);
            SceneManager.MoveGameObjectToScene(result, SceneManager.GetActiveScene());
            return result;
        }
    }
}