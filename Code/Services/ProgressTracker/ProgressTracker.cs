using System;
using ProjectFiles.Code.Models;
using ProjectFiles.Code.MonoBehaviorEntities;

namespace ProjectFiles.Code.Services.ProgressTracker
{
    public class ProgressTracker : IProgressTracker
    {
        public GameDataModel GameDataModel { get; }
        private event Func<bool> CheckWordCompleted;
        private event Func<bool> CheckHangmanCompleted;
        
        public ProgressTracker(GameDataModel gameDataModel)
        {
            GameDataModel = gameDataModel;
        }
        
        public void SubscribeOnCheckWordCompleted(Func<bool> callback)
        {
            if (callback.Target is Word && CheckWordCompleted?.GetInvocationList() == null)
            {
                CheckWordCompleted += callback;
            }
        }

        public void SubscribeOnCheckHangmanCompleted(Func<bool> callback)
        {
            if (callback.Target is Hangman && CheckHangmanCompleted?.GetInvocationList() == null)
            {
                CheckHangmanCompleted += callback;
            }
        }

        public bool CheckWordCompletedInvoke()
        {
            if (CheckWordCompleted == null) 
                return false;

            return CheckWordCompleted.Invoke();
        }
        
        public bool CheckHangmanCompletedInvoke()
        {
            if (CheckHangmanCompleted == null) 
                return false;
            
            return CheckHangmanCompleted.Invoke();
        }

        public void UpdateGameData(bool playerWon)
        {
            if (playerWon)
                GameDataModel.PlayerWon();
            else
                GameDataModel.PlayerLost();
        }
    }
}