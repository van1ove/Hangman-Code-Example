using System;
using ProjectFiles.Code.Models;

namespace ProjectFiles.Code.Services.ProgressTracker
{
    public interface IProgressTracker : IService
    {
        public GameDataModel GameDataModel { get; }

        void SubscribeOnCheckWordCompleted(Func<bool> callback);
        void SubscribeOnCheckHangmanCompleted(Func<bool> callback);

        bool CheckWordCompletedInvoke();
        bool CheckHangmanCompletedInvoke();

        void UpdateGameData(bool playerWon);
    }
}