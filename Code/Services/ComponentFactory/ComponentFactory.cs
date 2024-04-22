using System;
using System.Collections.Generic;
using ProjectFiles.Code.Models.Consts;
using ProjectFiles.Code.MonoBehaviorEntities;
using ProjectFiles.Code.Services.AssetProvider;

namespace ProjectFiles.Code.Services.ComponentFactory
{
    public class ComponentFactory : IComponentFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<Type, string> _assetsDictionary = new Dictionary<Type, string>
        {
            [typeof(KeyboardItem)] = AssetsPaths.KeyboardItemPath,
            [typeof(LetterItem)] = AssetsPaths.LetterItemPath,
            [typeof(Keyboard)] = AssetsPaths.KeyboardPath,
            [typeof(Word)] = AssetsPaths.LettersListPath,
            [typeof(Hangman)] = AssetsPaths.HangmanPath,
            [typeof(GameRestarter)] = AssetsPaths.GameRestartPath
        };    

        public ComponentFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public T CreateComponentFromPrefab<T>() where T : IEntity =>
            _assetProvider.LoadAsset<T>(_assetsDictionary[typeof(T)]);
    }
}