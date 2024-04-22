using UnityEngine;

namespace ProjectFiles.Code.Services.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        public T LoadAsset<T>(string path) => 
            Resources.Load<GameObject>(path).GetComponent<T>();
    }
}