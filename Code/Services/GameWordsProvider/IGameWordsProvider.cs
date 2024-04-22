namespace ProjectFiles.Code.Services.GameWordsProvider
{
    public interface IGameWordsProvider : IService
    {
        string LoadWord();
        void DeleteWord();
    }
}