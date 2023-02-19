using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class HangmanGame : MonoBehaviour
{   
    
    
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _textForScript;
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private int hp = 7;
    [SerializeField] private GameObject[] hpImage;
    
    private List<char> guessedLetters = new List<char>();
    
    private List<char> wrongTriedLetter = new List<char>();

    private string[] words =
    {
        "Ball",
        "Girl",
        "Effect",
        "Mouse",
        "Glitch"
    };

    
    
    private string wordToGuess = "";

    private KeyCode lastKeyPressed;

    private void Start()
    {
        int randomIndex = Random.Range(0, words.Length);
        
        wordToGuess = words[randomIndex];
        
        string initialWord = "";
        for (int i = 0; i < wordToGuess.Length; i++)
        {
            initialWord += " _";
        }
        _textField.text = initialWord;
        _textForScript.text = wordToGuess.ToUpper();
        _hpText.text = hp.ToString();
    }
    
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            
            if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
            {
                ProcessKey(e.keyCode);
                
                lastKeyPressed = e.keyCode;
                //Debug.Log("detected key code:" + e.keyCode);
            }
        }
    }

    
    
    private void ProcessKey(KeyCode key)
    {
        print("Key Pressed: " + key);

        char pressedKeyString = key.ToString()[0];

        string wordUppercase = wordToGuess.ToUpper();

        bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);

        bool letterWasGuessed = guessedLetters.Contains(pressedKeyString);

        if (!wordContainsPressedKey && !wrongTriedLetter.Contains(pressedKeyString))
        {
            wrongTriedLetter.Add(pressedKeyString);
            hp -= 1;
            ChangeHpImage(hp);

            if (hp <= 0)
            {
                //SceneManager.LoadSceneAsync(sceneBuildIndex: 2);
                SceneManager.LoadScene("YouLose");
            }
            else
            {
                _hpText.text = hp.ToString();
                
            }
            
        }
        

        if (wordContainsPressedKey && !letterWasGuessed)
        {
            guessedLetters.Add(pressedKeyString);
        }

        string stringToPrint = "";

        for (int i = 0; i < wordUppercase.Length; i++)
        {
            char letterInWord = wordUppercase[i];

            if (guessedLetters.Contains(letterInWord))
            {
                stringToPrint += letterInWord;
            }
            else
            {
                stringToPrint += " _";
            }
        }

        if (wordUppercase == stringToPrint)
        {
            //SceneManager.LoadSceneAsync(sceneBuildIndex: 3);
            SceneManager.LoadScene("YouWin");
        }
        
        _textField.text = stringToPrint;
        
        void ChangeHpImage(int hpValue)
        {
            foreach (var image in hpImage)
            {
                image.SetActive(hpValue.ToString() == image.name);
            }
        }
    }
}