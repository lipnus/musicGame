using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {

    public Text nicknameText;
    public Text rankingText;
    public Text correctText;
    public Text percentText;
    public Text gameClearText;

    public List<GameObject> rankBox = new List<GameObject>();

    
    public ConnectServer connectServer;
    

    void Start() {
        setNicknameText();
        setCorrectText();
        setPercentText();
        setGameClearText();
    }

    public void initRank() {
        
        int correct = Utils.getCorrect();
        int wrong = Utils.getWrong();
        int gameClear = Utils.getGameClear();
        int score = 10 * gameClear + correct + wrong;
        connectServer.requestUserRank( score );
        
    }

    public void setRankBox() {

        for (int i = 0; i < rankBox.Count; i++) {
            rankBox[i].transform.Find("nicknameText");
            rankBox[i].transform.Find("scoreText");
            
            if (3 < i) {
                rankBox[i].transform.Find("rankText").GetComponent<Text>().text = (i+1)+"";
            }
        }
        
    }
    
    
    //connectText에서 호출
    public void setRankingText(int ranking) {
        rankingText.text = ranking + "등";
    }
    
    
    public void setNicknameText() {
        string nickname = Utils.getNickname();

        if (nickname != "") {
            nicknameText.text = nickname;
        }
        else {
            nicknameText.text = "게임을 시작해주세요";
        }
    }
    

    public void setCorrectText() {
        correctText.text = "맞힌 문제: " + Utils.getCorrect();
    }
    

    public void setPercentText() {
        float correct = Utils.getCorrect();
        float wrong = Utils.getWrong();
        if (wrong == 0) wrong=1;
        
        Debug.Log("틀린거: " +wrong + " 맞은거: " + correct);
        
        double percent = correct / (correct+wrong) * 100;
        percent = Math.Floor(percent);
        percentText.text = "정답률: " + percent  + "%";
    }
    

    public void setGameClearText() {
        gameClearText.text = "게임클리어: " + Utils.getGameClear();
    }
    
    
}
