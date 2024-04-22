using UnityEngine;

namespace ProjectFiles.Code.StateMachine.States
{
    public class LoadGamePanelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameObject _gamePanel;

        public LoadGamePanelState(GameStateMachine gameStateMachine, GameObject gamePanel)
        {
            _gameStateMachine = gameStateMachine;
            _gamePanel = gamePanel;
        }
        public void Enter()
        {
            _gamePanel.SetActive(true);
            _gameStateMachine.Enter<KeyboardState>();
        }

        public void Exit()
        { }
    }
}