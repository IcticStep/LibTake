using UnityEngine;

namespace Code.Runtime.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path, Vector3 at, Transform parent = null);
    }
}