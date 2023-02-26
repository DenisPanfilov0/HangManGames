using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class HangmanController : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI _hpText;
    [FormerlySerializedAs("_textForScript")] [SerializeField] private TextMeshProUGUI _textWordToGuess;
    [FormerlySerializedAs("_textField")] [SerializeField] private TextMeshProUGUI _textWordInLetters;
    [SerializeField] private int hp = 7;
    [SerializeField] private GameObject[] hpImage;
    [FormerlySerializedAs("_descriptionWords")] [SerializeField] private TextMeshProUGUI _textDescriptionWords;

    private readonly List<char> _guessedLetters = new();
    private readonly List<char> _wrongTriedLetter = new();

    private readonly string[] _descriptionWords =
    {
        "They play football",
        "She says moo",
        "She says meow",
        "Cats catch her",
        "Using this device people talk"
    };

    private readonly string[] _words =
    {
        "Ball",
        "Cow",
        "Cat",
        "Mouse",
        "Telephone"
    };



    private string _wordToGuess = "";

    private KeyCode _lastKeyPressed;

    private void Start()
    {
        int randomIndex = Random.Range(0, _words.Length);

        _wordToGuess = _words[randomIndex];
        _textDescriptionWords.text = _descriptionWords[randomIndex];

        string initialWord = "";
        for (int i = 0; i < _wordToGuess.Length; i++)
        {
            initialWord += " _";
        }

        _textWordInLetters.text = initialWord;
        _textWordToGuess.text = _wordToGuess.ToUpper();
        _hpText.text = hp.ToString();
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (!e.isKey) return;
        if (e.keyCode != KeyCode.None && _lastKeyPressed != e.keyCode)
        {
            ProcessKey(e.keyCode);

            _lastKeyPressed = e.keyCode;
        }
        
        /* СВЕРХУ ТО ЖЕ САМОЕ!!!!!
        Event e = Event.current;
        if (e.isKey)
        {

            if (e.keyCode != KeyCode.None && _lastKeyPressed != e.keyCode)
            {
                ProcessKey(e.keyCode);

                _lastKeyPressed = e.keyCode;
            }
        }*/
    }



    private void ProcessKey(KeyCode key)
    {
        print("Key Pressed: " + key);

        char pressedKeyString = key.ToString()[0];

        string wordUppercase = _wordToGuess.ToUpper();

        bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);

        bool letterWasGuessed = _guessedLetters.Contains(pressedKeyString);

        if (!wordContainsPressedKey && !_wrongTriedLetter.Contains(pressedKeyString))
        {
            _wrongTriedLetter.Add(pressedKeyString);
            hp -= 1;
            ChangeHpImage(hp);

            if (hp <= 0)
            {
                SceneManager.LoadScene("YouLose");
            }
            else
            {
                _hpText.text = hp.ToString();

            }

        }


        if (wordContainsPressedKey && !letterWasGuessed)
        {
            _guessedLetters.Add(pressedKeyString);
        }

        var stringToPrint = "";

        foreach (var letterInWord in wordUppercase)
        {
            if (_guessedLetters.Contains(letterInWord))
            {
                stringToPrint += letterInWord;
            }
            else
            {
                stringToPrint += " _";
            }
        }
        
        /* СВЕРХУ ТО ЖЕ САМОЕ!!!!!
        for (int i = 0; i < wordUppercase.Length; i++)
        {
            char letterInWord = wordUppercase[i];

            if (_guessedLetters.Contains(letterInWord))
            {
                stringToPrint += letterInWord;
            }
            else
            {
                stringToPrint += " _";
            }
        }*/

        if (wordUppercase == stringToPrint)
        {
            SceneManager.LoadScene("YouWin");
        }

        _textWordInLetters.text = stringToPrint;

        void ChangeHpImage(int hpValue)
        {
            foreach (var image in hpImage)
            {
                if (hpValue.ToString() == image.name)
                {
                    image.SetActive(true);
                }
            }
        }
    }
}