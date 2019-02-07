using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour {

    public InputField inputField;
    public ConnectServer connectServer;
    public Text sleepingText;

    private string nickname; 
    
    
    // Start is called before the first frame update
    void Start() {
        inputField.interactable = true;
    }

    public void nicknameChanged() {
        if (inputField.text != "") {
            sleepingText.text = "이곳은 " + inputField.text + "의 꿈 속";
        }
        else {
            sleepingText.text = "이곳은 꿈 속";
        }
    }

    //닉네임 등록하기
    public void onClick_enroll() {
        if (inputField.text.Equals("")) {
            sleepingText.text = "입력을 부탁해요";
            return;
        }
        
        nickname = inputField.text;
        connectServer.nicknameCheck( nickname );
    }
    
    //콜백
    public void nicknameOK() {
        Utils.setNickname( nickname );
        SceneManager.LoadScene("HomeScene");
    }

    //콜백
    public void nicknameReject() {
        sleepingText.text = "이미 존재하는 닉네임";
    }
}
