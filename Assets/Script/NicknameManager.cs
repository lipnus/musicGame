using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour {

    public InputField inputField;
    public Text sleepingText;
    
    
    // Start is called before the first frame update
    void Start() {
        inputField.interactable = true;
    }

    public void nicknameChanged() {
        if (inputField.text != "") {
            sleepingText.text = "이곳은 " + inputField.text + "의 꿈속";
        }
        else {
            sleepingText.text = "이곳은 꿈속";
        }
    }
}
