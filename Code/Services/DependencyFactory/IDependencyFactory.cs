using ProjectFiles.Code.MonoBehaviorEntities;
using UnityEngine;

namespace ProjectFiles.Code.Services.DependencyFactory
{
    public interface IDependencyFactory : IService
    {
        T CreateComponentFromPrefab<T>(Transform position) where T : MonoBehaviour, IEntity;
    }
}