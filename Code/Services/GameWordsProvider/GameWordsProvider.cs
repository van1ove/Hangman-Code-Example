using System.Collections.Generic;
using System.Linq;
using ProjectFiles.Code.Models.Consts;
using Random = System.Random;

namespace ProjectFiles.Code.Services.GameWordsProvider
{
    public class GameWordsProvider : IGameWordsProvider
    {
        private readonly GameWords _gameWords;
        private List<string> _unusedWords;
        private string _lastProvidedString;
        
        public GameWordsProvider(GameWords gameWords)
        {
            _gameWords = gameWords;
            _unusedWords = new List<string>(_gameWords.Words).Distinct().ToList();
        }

        public string LoadWord()
        {
            if (_unusedWords.Count == 0)
            {
                _unusedWords = new List<string>(_gameWords.Words).Distinct().ToList();
                return LoadWord();
            }
            
            Random rand = new Random();
            int randomIndex = rand.Next(0, _unusedWords.Count);
            _lastProvidedString = _unusedWords[randomIndex];
            return _lastProvidedString;
        }

        public void DeleteWord()
        {
            int index = _unusedWords.FindIndex(x => x == _lastProvidedString);
            _unusedWords.RemoveAt(index);
        }
    }
}