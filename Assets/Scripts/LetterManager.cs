using TMPro;
using UnityEngine;

public class LetterManager : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI _guesedWord;
    [SerializeField] private GameObject letterClosure;
    [SerializeField] private TextMeshProUGUI _keyText;

    private float _countDown;  

    private KeyCode _lastKeyPressed;

    
    

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {

            if (e.keyCode != KeyCode.None && _lastKeyPressed != e.keyCode)
            {
                _lastKeyPressed = e.keyCode;
                ProcessKey(e.keyCode);
            }
        }
    }

    private void ProcessKey(KeyCode key)
    {
        char pressedKeyString = key.ToString()[0];

        string wordUppercase = _guesedWord.text.ToUpper();
        
       
        if (!wordUppercase.Contains(pressedKeyString) && pressedKeyString == _keyText.text[0])
        {
            letterClosure.SetActive(true);
        }
        
    }

    
    
} 
