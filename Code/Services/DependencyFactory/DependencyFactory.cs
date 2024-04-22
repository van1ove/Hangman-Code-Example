using ProjectFiles.Code.MonoBehaviorEntities;
using ProjectFiles.Code.Services.ComponentFactory;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Code.Services.DependencyFactory
{
    public class DependencyFactory : IDependencyFactory
    {
        private readonly IComponentFactory _componentFactory;
        private readonly DiContainer _diContainer;

        public DependencyFactory(IComponentFactory componentFactory, DiContainer diContainer)
        {
            _componentFactory = componentFactory;
            _diContainer = diContainer;
        }
        
        public T CreateComponentFromPrefab<T>(Transform position) where T : MonoBehaviour, IEntity =>
            _diContainer.InstantiatePrefab(_componentFactory.CreateComponentFromPrefab<T>(), position).GetComponent<T>();
    }
}