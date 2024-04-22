using ProjectFiles.Code.MonoBehaviorEntities;
using ProjectFiles.Code.Services.DependencyFactory;
using ProjectFiles.Code.Services.ProgressTracker;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.Code.StateMachine.States
{
    public class ResultState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IDependencyFactory _dependencyFactory;
        private readonly IProgressTracker _progressTracker;
        private readonly Transform _gameStateTransform;
        private readonly Text _resultText;

        private GameRestarter _gameRestarter;
        
        public ResultState(GameStateMachine gameStateMachine, IDependencyFactory dependencyFactory, 
            IProgressTracker progressTracker,
            Transform gameStateTransform, Text resultText)
        {
            _gameStateMachine = gameStateMachine;
            _dependencyFactory = dependencyFactory;
            _progressTracker = progressTracker;
            _gameStateTransform = gameStateTransform;
            _resultText = resultText;
        }

        public void Enter()
        {
            CheckGameRestarter();
            _gameRestarter.SetResultText(_progressTracker.GameDataModel.IsPlayerWon);
            _resultText.text = 
                $"Выиграно: {_progressTracker.GameDataModel.VictoriesAmount}. " +
                $"Проиграно: {_progressTracker.GameDataModel.DefeatsAmount}. ";
        }

        public void Exit()
        {
            _gameRestarter.gameObject.SetActive(false);
        }

        private void CheckGameRestarter()
        {
            if (_gameRestarter == null)
            {
                _gameRestarter = _dependencyFactory.CreateComponentFromPrefab<GameRestarter>(_gameStateTransform);
                _gameRestarter.AddButtonListener(_gameStateMachine.Enter<KeyboardState>);
            }
            else
            {
                _gameRestarter.gameObject.SetActive(true);
            }
        }
    }
}