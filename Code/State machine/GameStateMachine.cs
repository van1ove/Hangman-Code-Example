using System;
using System.Collections.Generic;
using ProjectFiles.Code.Services.ComponentFactory;
using ProjectFiles.Code.Services.DependencyFactory;
using ProjectFiles.Code.Services.GameWordsProvider;
using ProjectFiles.Code.Services.ProgressTracker;
using ProjectFiles.Code.StateMachine.States;
using ProjectFiles.Code.UI;

namespace ProjectFiles.Code.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _currentState;

        public GameStateMachine(
            UIHandler uiHandler,
            IComponentFactory componentFactory,
            IDependencyFactory dependencyFactory,
            IGameWordsProvider gameWordsProvider,
            IProgressTracker progressTracker)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(LoadMenuPanelState)] = new LoadMenuPanelState(this, 
                    uiHandler.MenuPanel),
                [typeof(LoadGamePanelState)] = new LoadGamePanelState(this, 
                    uiHandler.GamePanel),
                [typeof(KeyboardState)] = new KeyboardState(this, 
                    componentFactory, gameWordsProvider, progressTracker,
                    uiHandler.GameStatesTransform, uiHandler.GameAreaTransform),
                [typeof(ResultState)] = new ResultState(this, 
                    dependencyFactory, progressTracker, 
                    uiHandler.GameStatesTransform, uiHandler.ResultText)
            };
        }

        public void Enter<TState>() where TState : IState
        {
            IState newState = ChangeState<TState>();
            newState.Enter();
        }

        private TState ChangeState<TState>() where TState : IState
        {
            _currentState?.Exit();
            TState newState = (TState)_states[typeof(TState)];
            _currentState = newState;

            return newState;
        }
    }
}
