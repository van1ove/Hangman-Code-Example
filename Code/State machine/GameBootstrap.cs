using ProjectFiles.Code.StateMachine.States;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Code.StateMachine
{
    public class GameBootstrap : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;
        
        private void Awake()
        {
            _gameStateMachine.Enter<LoadMenuPanelState>();
        }
    }
}