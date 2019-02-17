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
    
    //메인에서 아이콘을 누를때 호출
    public void initRank() {
        
        int correct = Utils.getCorrect();
        int wrong = Utils.getWrong();
        int gameClear = Utils.getGameClear();
        int score = 10 * gameClear + correct - wrong;
        
        connectServer.requestUserRank( score );
        connectServer.requestAccumulateRanking( rankBox.Count );
        
    }

    //connectServer에서 호출
    public void setRankBox( List<Ranking> rankings ) {

        for (int i = 0; i < rankBox.Count; i++) {
            rankBox[i].transform.Find("nicknameText").GetComponent<Text>().text = rankings[i].nickname;
            rankBox[i].transform.Find("scoreText").GetComponent<Text>().text = rankings[i].score.ToString();

            
            if (2 < i) {
                rankBox[i].transform.Find("rankText").GetComponent<Text>().text = (i+1).ToString();
                rankBox[i].transform.Find("nicknameText").GetComponent<Text>().text = rankings[i].nickname;
                rankBox[i].transform.Find("scoreText").GetComponent<Text>().text = rankings[i].score.ToString();
            }
        }

    }
    
    
    //connectServer에서 호출
    public void setUserRankingText(int ranking) {
        rankingText.text = ranking + "등";
    }
    
    
    public void setNicknameText() {
        string nickname = Utils.getNickname();

        if (nickname != "empty_nickname") {
            nicknameText.text = nickname;
        }
        else {
            nicknameText.text = "Noname";
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
