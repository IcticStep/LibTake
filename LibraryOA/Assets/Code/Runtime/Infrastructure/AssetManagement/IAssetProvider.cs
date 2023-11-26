using UnityEngine;

namespace Code.Runtime.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(GameObject prefab, Transform parent = null);
        GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent = null);
        GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent = null);
        GameObject Instantiate(string path, Transform parent = null);
        GameObject Instantiate(string path, Vector3 at, Transform parent = null);
        GameObject Instantiate(string path, Vector3 at, Quaternion rotation, Transform parent = null);
    }
}