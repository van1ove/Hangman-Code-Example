using System.Linq;
using ProjectFiles.Code.Models.Consts;
using ProjectFiles.Code.MonoBehaviorEntities;
using ProjectFiles.Code.Services.ComponentFactory;
using ProjectFiles.Code.Services.GameWordsProvider;
using ProjectFiles.Code.Services.ProgressTracker;
using UnityEngine;

namespace ProjectFiles.Code.StateMachine.States
{
    public class KeyboardState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IComponentFactory _componentFactory;
        private readonly IGameWordsProvider _gameWordsProvider;
        private readonly IProgressTracker _progressTracker;
        
        private readonly Transform _keyboardTransform;
        private readonly Transform _gameAreaTransform;
        
        private Keyboard _keyboard;
        private Word _word;
        private Hangman _hangman;

        public KeyboardState(GameStateMachine gameStateMachine, IComponentFactory componentFactory, 
            IGameWordsProvider gameWordsProvider, IProgressTracker progressTracker,
            Transform keyboardTransform, Transform gameAreaTransform)
        {
            _gameStateMachine = gameStateMachine;
            _componentFactory = componentFactory;
            _gameWordsProvider = gameWordsProvider;
            _progressTracker = progressTracker;
            _keyboardTransform = keyboardTransform;
            _gameAreaTransform = gameAreaTransform;
        }
        public void Enter()
        {
            CheckKeyboard();
            CheckWord();
            CheckHangman();
        }

        public void Exit()
        {
            _keyboard.gameObject.SetActive(false);
        }
        
        private void CheckKeyboard()
        {
            if (_keyboard == null) 
                _keyboard = SpawnKeyboard(_componentFactory.CreateComponentFromPrefab<Keyboard>());
            else
            {
                _keyboard.gameObject.SetActive(true);
                _keyboard.ResetLettersStatus();
            }
        }

        private void CheckWord()
        {
            if (_word == null)
            {
                _word = SpawnWord(_componentFactory.CreateComponentFromPrefab<Word>());
                _progressTracker.SubscribeOnCheckWordCompleted(_word.AllLettersShown);
            }
            else
            {
                _word.DestroyAllItems();
                SetLettersForWord(_word);
            }
        }

        private void CheckHangman()
        {
            if (_hangman == null)
            {
                _hangman = Object.Instantiate(_componentFactory.CreateComponentFromPrefab<Hangman>(), _gameAreaTransform);
                _progressTracker.SubscribeOnCheckHangmanCompleted(_hangman.CheckShownComponentsAmount);
            }
            _hangman.ResetHangman();
        }
        
        private Keyboard SpawnKeyboard(Keyboard keyboardExample)
        {
            Keyboard keyboard = Object.Instantiate(keyboardExample, _keyboardTransform);
            
            KeyboardItem keyboardItemPrefab = _componentFactory.CreateComponentFromPrefab<KeyboardItem>();
            foreach (char letter in AlphabetModel.Alphabet)
            {
                keyboardItemPrefab.Initialize(letter);
                
                KeyboardItem keyboardItem = Object.Instantiate(keyboardItemPrefab, keyboard.transform);
            
                keyboardItem.Initialize(letter);
                keyboardItem.SubscribeOnButton(KeyboardItemClick);
                
                keyboard.AddLetterStatus(letter);
            }

            return keyboard;
        }
        
        private Word SpawnWord(Word wordExample)
        {
            Word word = Object.Instantiate(wordExample, _gameAreaTransform);
            return SetLettersForWord(word);
        }

        private Word SetLettersForWord(Word word)
        {
            LetterItem letterPrefab = _componentFactory.CreateComponentFromPrefab<LetterItem>();
            
            foreach (char letter in _gameWordsProvider.LoadWord())
            {
                letterPrefab.Initialize(letter);
                LetterItem letterItem = Object.Instantiate(letterPrefab, word.transform);
                letterItem.Initialize(letter);
                word.AddItem(letterItem);
            }
            return word;
        }

        private void KeyboardItemClick(KeyboardItem item)
        {
            if(_keyboard.AlphabetLettersStatus[item.Letter])
                return;

            bool letterInWord = false;
            foreach (var letterItem in _word.Items.Where(letterItem => letterItem.Letter == item.Letter))
            {
                letterItem.ShowLetter();
                letterInWord = true;
            }
            
            if(!letterInWord)
                _hangman.ShowNextComponent();

            _keyboard.AlphabetLettersStatus[item.Letter] = true;
            CheckIsGameEnded();
        }

        private void CheckIsGameEnded()
        {
            if (_progressTracker.CheckWordCompletedInvoke())
            {
                _progressTracker.UpdateGameData(true);
                _gameWordsProvider.DeleteWord();
                _hangman.ResetHangman();
                _gameStateMachine.Enter<ResultState>();
            }

            if (_progressTracker.CheckHangmanCompletedInvoke())
            {
                _progressTracker.UpdateGameData(false);
                _gameStateMachine.Enter<ResultState>();
            }
        }
    }
}