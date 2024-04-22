using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.Code.StateMachine.States
{
    public class LoadMenuPanelState : IState
    {
        private readonly GameObject _menuPanel;
        public LoadMenuPanelState(GameStateMachine gameStateMachine, GameObject menuPanel)
        {
            _menuPanel = menuPanel;
            _menuPanel.GetComponentInChildren<Button>().
                onClick.AddListener(gameStateMachine.Enter<LoadGamePanelState>);
        }
        public void Enter()
        {
            _menuPanel.SetActive(true);
        }

        public void Exit()
        {
            _menuPanel.SetActive(false);
        }
    }
}