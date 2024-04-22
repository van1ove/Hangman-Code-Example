namespace ProjectFiles.Code.Services.AssetProvider
{
    public interface IAssetProvider : IService
    {
        T LoadAsset<T> (string path);
    }
}