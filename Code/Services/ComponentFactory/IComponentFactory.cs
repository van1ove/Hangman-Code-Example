using ProjectFiles.Code.MonoBehaviorEntities;

namespace ProjectFiles.Code.Services.ComponentFactory
{
    public interface IComponentFactory : IService
    {
        T CreateComponentFromPrefab<T>() where T : IEntity;
    }
}
