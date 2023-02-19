using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Letter : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI _guesedWord;
    [SerializeField] private GameObject letterClosure;
    [SerializeField] private TextMeshProUGUI _keyText;

    private float countDown;  

    private KeyCode lastKeyPressed;

    
    

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {

            if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
            {
                lastKeyPressed = e.keyCode;
                ProcessKey(e.keyCode);
            }
        }
    }

    public void ProcessKey(KeyCode key)
    {
        char pressedKeyString = key.ToString()[0];

        string wordUppercase = _guesedWord.text.ToUpper();
        
       
        if (!wordUppercase.Contains(pressedKeyString) && pressedKeyString == _keyText.text[0])
        {
            letterClosure.SetActive(true);
        }
        
    }

    
    
} 
